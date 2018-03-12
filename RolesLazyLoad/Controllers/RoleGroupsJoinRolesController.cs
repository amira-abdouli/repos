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
    public class RoleGroupsJoinRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoleGroupsJoinRoles
        public ActionResult Index()
        {
            var roleGroupsJoinRoles = db.RoleGroupsJoinRoles.Include(r => r.RoleGroups).Include(r => r.Roles);
            return View(roleGroupsJoinRoles.ToList());
        }

        // GET: RoleGroupsJoinRoles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleGroupsJoinRoles roleGroupsJoinRoles = db.RoleGroupsJoinRoles.Find(id);
            if (roleGroupsJoinRoles == null)
            {
                return HttpNotFound();
            }
            return View(roleGroupsJoinRoles);
        }

        // GET: RoleGroupsJoinRoles/Create
        public ActionResult Create()
        {
            ViewBag.RoleGroupsID = new SelectList(db.RoleGroups, "ID", "Name");
            ViewBag.RolesID = new SelectList(db.Roles, "ID", "Name");
            return View();
        }

        // POST: RoleGroupsJoinRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RolesID,RoleGroupsID")] RoleGroupsJoinRoles roleGroupsJoinRoles)
        {
            if (ModelState.IsValid)
            {
                roleGroupsJoinRoles.RolesID = Guid.NewGuid();
                db.RoleGroupsJoinRoles.Add(roleGroupsJoinRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleGroupsID = new SelectList(db.RoleGroups, "ID", "Name", roleGroupsJoinRoles.RoleGroupsID);
            ViewBag.RolesID = new SelectList(db.Roles, "ID", "Name", roleGroupsJoinRoles.RolesID);
            return View(roleGroupsJoinRoles);
        }

        // GET: RoleGroupsJoinRoles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleGroupsJoinRoles roleGroupsJoinRoles = db.RoleGroupsJoinRoles.Find(id);
            if (roleGroupsJoinRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleGroupsID = new SelectList(db.RoleGroups, "ID", "Name", roleGroupsJoinRoles.RoleGroupsID);
            ViewBag.RolesID = new SelectList(db.Roles, "ID", "Name", roleGroupsJoinRoles.RolesID);
            return View(roleGroupsJoinRoles);
        }

        // POST: RoleGroupsJoinRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RolesID,RoleGroupsID")] RoleGroupsJoinRoles roleGroupsJoinRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roleGroupsJoinRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleGroupsID = new SelectList(db.RoleGroups, "ID", "Name", roleGroupsJoinRoles.RoleGroupsID);
            ViewBag.RolesID = new SelectList(db.Roles, "ID", "Name", roleGroupsJoinRoles.RolesID);
            return View(roleGroupsJoinRoles);
        }

        // GET: RoleGroupsJoinRoles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleGroupsJoinRoles roleGroupsJoinRoles = db.RoleGroupsJoinRoles.Find(id);
            if (roleGroupsJoinRoles == null)
            {
                return HttpNotFound();
            }
            return View(roleGroupsJoinRoles);
        }

        // POST: RoleGroupsJoinRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            RoleGroupsJoinRoles roleGroupsJoinRoles = db.RoleGroupsJoinRoles.Find(id);
            db.RoleGroupsJoinRoles.Remove(roleGroupsJoinRoles);
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
