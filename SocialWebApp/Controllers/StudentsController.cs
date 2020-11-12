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

namespace SocialWebApp.Controllers
{
    public class StudentsController : Controller
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

        // GET: Students
        public async Task<ActionResult> Index()
        {

            var user = UserManager.FindById(User.Identity.GetUserId());
            var courses = db.Students.Where(c => c.EducationCenterID == user.Id);
            return View(await courses.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,LastName,FirstMidName,Email,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                student.EducationCenterID = User.Identity.GetUserId();
                student.Password = "password";
                MailMessage m = new MailMessage(
             new MailAddress("arthour.martirosyan@gmail.com", "Arthour Martirosyan"),
             new MailAddress(student.Email));
                if (m != null)
                {
                    m.Subject = "Nodification from me";
                    m.Body = string.Format("Your Password is:"+student.Password,
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
                //student.Password = await SignInManager.SendTwoFactorCodeAsync(student);
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", student.EducationCenterID);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);

            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", student.EducationCenterID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,LastName,FirstMidName,Email,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.EducationCenterID = User.Identity.GetUserId();

                db.Entry(student).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", student.EducationCenterID);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Student student = await db.Students.FindAsync(id);
            student.EducationCenterID = User.Identity.GetUserId();

            db.Students.Remove(student);
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

    }
}
