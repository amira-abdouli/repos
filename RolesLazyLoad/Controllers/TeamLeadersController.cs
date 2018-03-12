using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RolesLazyLoad.Models;

namespace RolesLazyLoad.Controllers
{
    public class TeamLeadersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TeamLeaders
        public ActionResult Index()
        {
            var teamLeaders = db.TeamLeaders.Include(t => t.Teams);
            return View(teamLeaders.ToList());
        }

        // GET: TeamLeaders/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamLeaders teamLeaders = db.TeamLeaders.Find(id);
            if (teamLeaders == null)
            {
                return HttpNotFound();
            }
            return View(teamLeaders);
        }

        // GET: TeamLeaders/Create
        public ActionResult Create()
        {
            ViewBag.TeamsID = new SelectList(db.Teams, "ID", "TeamName");
            return View();
        }

        // POST: TeamLeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TeamLeaderName,TeamsID")] TeamLeaders teamLeaders)
        {
            if (ModelState.IsValid)
            {
                teamLeaders.ID = Guid.NewGuid();
                db.TeamLeaders.Add(teamLeaders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TeamsID = new SelectList(db.Teams, "ID", "TeamName", teamLeaders.TeamsID);
            return View(teamLeaders);
        }

        // GET: TeamLeaders/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamLeaders teamLeaders = db.TeamLeaders.Find(id);
            if (teamLeaders == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamsID = new SelectList(db.Teams, "ID", "TeamName", teamLeaders.TeamsID);
            return View(teamLeaders);
        }

        // POST: TeamLeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TeamLeaderName,TeamsID")] TeamLeaders teamLeaders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamLeaders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TeamsID = new SelectList(db.Teams, "ID", "TeamName", teamLeaders.TeamsID);
            return View(teamLeaders);
        }

        // GET: TeamLeaders/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamLeaders teamLeaders = db.TeamLeaders.Find(id);
            if (teamLeaders == null)
            {
                return HttpNotFound();
            }
            return View(teamLeaders);
        }

        // POST: TeamLeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TeamLeaders teamLeaders = db.TeamLeaders.Find(id);
            db.TeamLeaders.Remove(teamLeaders);
            db.SaveChanges();
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
