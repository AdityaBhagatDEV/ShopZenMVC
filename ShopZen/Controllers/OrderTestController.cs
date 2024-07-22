using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopZen.Models;

namespace ShopZen.Controllers
{
    public class OrderTestController : Controller
    {
        private ShopZenEntities1 db = new ShopZenEntities1();

        // GET: OrderTest
        public ActionResult Index()
        {
            var orderTables = db.OrderTables.Include(o => o.UserTable);
            return View(orderTables.ToList());
        }

        // GET: OrderTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        // GET: OrderTest/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.UserTables, "UserId", "FirstName");
            return View();
        }

        // POST: OrderTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,UserId,OrderDate,TotalAmount,Status")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.OrderTables.Add(orderTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserTables, "UserId", "FirstName", orderTable.UserId);
            return View(orderTable);
        }

        // GET: OrderTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserTables, "UserId", "FirstName", orderTable.UserId);
            return View(orderTable);
        }

        // POST: OrderTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,UserId,OrderDate,TotalAmount,Status")] OrderTable orderTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserTables, "UserId", "FirstName", orderTable.UserId);
            return View(orderTable);
        }

        // GET: OrderTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        // POST: OrderTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTable orderTable = db.OrderTables.Find(id);
            db.OrderTables.Remove(orderTable);
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
