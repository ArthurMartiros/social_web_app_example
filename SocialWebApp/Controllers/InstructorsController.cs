using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialWebApp.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using SocialWebApp.ViewModels;
using System.Data.Entity.Infrastructure;

namespace SocialWebApp.Controllers
{
    public class InstructorsController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructors
        public async Task<ActionResult> Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var instructors= db.Instructors.Where(c => c.EducationCenterID == user.Id);
            return View(await instructors.ToListAsync());
            //var instructors = db.Instructors.Include(i => i.EducationCenter).Include(i => i.OfficeAssignment);
            //return View(await instructors.ToListAsync());
        }

        // GET: Instructors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = await db.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName");
            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location");
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //var instructor = user.Instructors.Where(c => c.EducationCenterID == user.Id);
            var instructor = new Instructor();
            instructor.Courses = new List<Course>();

            PopulateAssignedCourseData(instructor);

            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,LastName,FirstMidName,Email,HireDate")] Instructor instructor, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                instructor.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    instructor.Courses.Add(courseToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                IdentityResult result;
                instructor.EducationCenterID = User.Identity.GetUserId();
                instructor.Password = "password";
                MailMessage m = new MailMessage(
             new MailAddress("arthour.martirosyan@gmail.com", "Arthour Martirosyan"),
             new MailAddress(instructor.Email));
                if (m != null)
                {
                    m.Subject = "Notification from me for Instructor";
                    m.Body = string.Format("Your Password is:" + instructor.Password,
                        Url.Action("Password", Request.Url.Scheme));
                    m.IsBodyHtml = true;
                }
                else
                {
                    result = IdentityResult.Failed("Sorry some problem occured", "Please try again");
                    AddErrors(result);
                }
                SmtpClient smtp = new SmtpClient("smtp.sendgrid.net");
                if (smtp != null)
                {
                    if (smtp != null)
                    {
                        try
                        {
                            smtp.Credentials = new NetworkCredential("arthour", "A.poker.6");
                            smtp.Port = 587;
                            smtp.EnableSsl = true;
                            smtp.Send(m);

                        }
                        catch
                        {
                            result = IdentityResult.Failed("your internet connection is not avelibale or very slow");
                            AddErrors(result);
                        }

                    }
                    else
                        return View("Error");
                }
                else
                {
                    result = IdentityResult.Failed("Sorry some problem occured", "The SMTP server works incorrect",
                        "Please try again");
                    AddErrors(result);
                }

                db.Instructors.Add(instructor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", instructor.EducationCenterID);
            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Instructor instructor = await db.Instructors.FindAsync(id);
            var user = UserManager.FindById(User.Identity.GetUserId());

            Instructor instructor =  await db.Instructors
               .Where(i=>i.EducationCenterID==user.Id)
                .Include(i => i.Courses)
               .Where(i => i.ID == id)
               .SingleAsync();


            PopulateAssignedCourseData(instructor);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", instructor.EducationCenterID);
            ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            //instructor.EducationCenterID = User.Identity.GetUserId();
            var user = UserManager.FindById(User.Identity.GetUserId());

            var instructorToUpdate =  db.Instructors
                .Where(i=>i.EducationCenterID==user.Id)
               .Include(i => i.Courses)
               .Where(i => i.ID == id)
               .Single();
            if (instructorToUpdate != null)
            {
                if (TryUpdateModel(instructorToUpdate, "",
                   new string[] { "LastName", "FirstMidName", "HireDate" }))
                {
                    try
                    {

                        UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                        db.Entry(instructorToUpdate).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                    catch (RetryLimitExceededException /* dex */)
                    {
                        //Log the error (uncomment dex variable name and add a line here to write a log.
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            else
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);

            //ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", instructor.EducationCenterID);
            //ViewBag.ID = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.ID);
            //return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = await db.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Instructor instructor = await db.Instructors.FindAsync(id);
            instructor.EducationCenterID = User.Identity.GetUserId();

            db.Instructors.Remove(instructor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            //var instructor = user.Instructors.Where(c => c.EducationCenterID == user.Id);
            var allCourses = db.Courses.Where(c=>c.EducationCenterID==user.Id);
            var instructorCourses = new HashSet<int>( instructor.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }


        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
    }
}
