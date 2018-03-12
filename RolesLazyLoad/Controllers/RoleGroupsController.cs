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
    public class RoleGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        //Create a partial view
        //get
        public ActionResult RolesGroup()
        {
            return PartialView();
        }


        //Create a delete ActionResult
        //without post method
        public ActionResult RolesGroupDelete(Guid RoleGroupsID, Guid RolesID)
        {
            var rolegroupsjoinroles = db.RoleGroupsJoinRoles.Where(c => c.RolesID == RolesID && c.RoleGroupsID == RoleGroupsID).SingleOrDefault();
            if (rolegroupsjoinroles != null)
            {
                db.RoleGroupsJoinRoles.Remove(rolegroupsjoinroles);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = RoleGroupsID });
        }


        //Create a create ActionResult
        //get
        public ActionResult RolesGroupCreate(Guid id)
        {
            return View(new RoleGroupsJoinRoles() { RoleGroupsID = id });
        }

        //post
        [HttpPost]
        public ActionResult RolesGroupCreate(RoleGroupsJoinRoles model)
        {
            model.Roles.ID = Guid.NewGuid();
            db.RoleGroupsJoinRoles.Add(model);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = model.RoleGroupsID });
        }

        //second part

        //get RoleGroupsJoinUsersList view requete to bring the list of group roles
        public ActionResult RoleGroupsJoinUsersList(Guid id)
        {
            return PartialView(db.RoleGroupsJoinUsers.Where(c => c.RoleGroupsID == id).ToList());
        }

        //AddUserToGroup drop down list
        //get
        public ActionResult AddUserToGroup(Guid id)
        {
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View(new RoleGroupsJoinUsers() { RoleGroupsID = id });
        }
        //post
        [HttpPost]
        public ActionResult AddUserToGroup(RoleGroupsJoinUsers model)
        {
            if (ModelState.IsValid)
            {
                db.RoleGroupsJoinUsers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = model.RoleGroupsID });
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserName");
            return View(new RoleGroupsJoinUsers() { RoleGroupsID = model.RoleGroupsID });
        }


        // GET: RoleGroups
        public ActionResult Index()
        {
            return View(db.RoleGroups.ToList());
        }

        // GET: RoleGroups/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleGroups roleGroups = db.RoleGroups.Find(id);
            if (roleGroups == null)
            {
                return HttpNotFound();
            }
            return View(roleGroups);
        }

        // GET: RoleGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] RoleGroups roleGroups)
        {
            if (ModelState.IsValid)
            {
                roleGroups.ID = Guid.NewGuid();
                db.RoleGroups.Add(roleGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roleGroups);
        }

        // GET: RoleGroups/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleGroups roleGroups = db.RoleGroups.Find(id);
            if (roleGroups == null)
            {
                return HttpNotFound();
            }
            return View(roleGroups);
        }

        // POST: RoleGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] RoleGroups roleGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roleGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roleGroups);
        }

        // GET: RoleGroups/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleGroups roleGroups = db.RoleGroups.Find(id);
            if (roleGroups == null)
            {
                return HttpNotFound();
            }
            return View(roleGroups);
        }

        // POST: RoleGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            RoleGroups roleGroups = db.RoleGroups.Find(id);
            db.RoleGroups.Remove(roleGroups);
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
