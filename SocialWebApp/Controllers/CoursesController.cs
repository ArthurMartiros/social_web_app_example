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



namespace SocialWebApp.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationUserManager _userManager;

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

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Courses
        public async Task<ActionResult> Index()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var courses = db.Courses.Where(c=>c.EducationCenterID==user.Id);
            return View(await courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            var user = UserManager.FindById(User.Identity.GetUserId());
            var instructors = await db.Instructors.Where(i => i.EducationCenterID == user.Id).ToListAsync();
            //course.Instructors = await db.Instructors.
            //    Include(i => i.Courses.Select(c => c.Instructors)).ToListAsync();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseID,Title,Credits,StartDate,FinishDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.EducationCenterID = User.Identity.GetUserId();
                db.Courses.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", course.EducationCenterID);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", course.EducationCenterID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseID,Title,Credits,StartDate,FinishDate")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.EducationCenterID = User.Identity.GetUserId();
                db.Entry(course).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EducationCenterID = new SelectList(db.Users, "Id", "CenterName", course.EducationCenterID);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = await db.Courses.FindAsync(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            
            Course course = await db.Courses.FindAsync(id);
            course.EducationCenterID = User.Identity.GetUserId();
            db.Courses.Remove(course);
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
    }
}
