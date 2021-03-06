﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eGymClass.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;

namespace eGymClass.Controllers
{
    public class GymClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GymClasses
        public ActionResult IndexAll()
        {
            var model = db.GymClasses.ToList();
            return View(model);
        }

        public ActionResult Index()
        {
            var model = db.GymClasses.Where(c => c.StartTime >= DateTime.Now).ToList();
            return View(model);
        }

        // GET: GymClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.GymClasses.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // GET: GymClasses/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.GymClasses.Add(gymClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        [Authorize(Roles ="admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.GymClasses.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gymClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymClass gymClass = db.GymClasses.Find(id);
            if (gymClass == null)
            {
                return HttpNotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GymClass gymClass = db.GymClasses.Find(id);
            db.GymClasses.Remove(gymClass);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult BookingToggle(int id)
        {
            GymClass currentClass = db.GymClasses.Where(g => g.Id == id).FirstOrDefault();
            ApplicationUser currentUser = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            if (currentClass.AttendingMembers.Contains(currentUser))
            {
                currentClass.AttendingMembers.Remove(currentUser);
                db.SaveChanges();
            }
            else
            {
                currentClass.AttendingMembers.Add(currentUser);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new {id = id });
        }

        [Authorize]
        public ActionResult BookedClass()
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var userclass = user.AttendedClasses.Where(c => c.StartTime >= DateTime.Now);
            return View("ClassList",userclass);
                
        }

        [Authorize]
        public ActionResult History()
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var userclass = user.AttendedClasses.Where(c => c.StartTime < DateTime.Now);
            return View("ClassList", userclass);

        }

        //public ActionResult test()
        //{
        //    db.GymClasses.AddOrUpdate(c => c.Name,
        //       new GymClass { Name = "Day Class 1", Description = "Morning Class", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(45) },
        //       new GymClass { Name = "Day Class 2", Description = "Morning Class", StartTime = DateTime.Now.AddHours(5), Duration = TimeSpan.FromMinutes(30) },
        //       new GymClass { Name = "Day Class 3", Description = "Morning Class", StartTime = DateTime.Now.AddDays(2), Duration = TimeSpan.FromMinutes(60) },
        //       new GymClass { Name = "Evening Class 1", Description = "Evening Class", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(50) },
        //       new GymClass { Name = "Evening Class 2", Description = "Evening Class", StartTime = DateTime.Now.AddHours(5), Duration = TimeSpan.FromMinutes(90) },
        //       new GymClass { Name = "Evening Class 3", Description = "Evening Class", StartTime = DateTime.Now.AddDays(2), Duration = TimeSpan.FromMinutes(70) }

        //       );
        //    return View();
        //}


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
