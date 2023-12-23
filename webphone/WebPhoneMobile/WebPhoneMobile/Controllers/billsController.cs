using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPhoneMobile.Models;

namespace WebPhoneMobile.Controllers
{
    [Authorize(Roles = "Admin")]
    public class billsController : Controller
    {
        private WebPhoneMobileEntities db = new WebPhoneMobileEntities();

        // GET: bills
        public ActionResult Index()
        {
            var bill = db.bill.Include(b => b.client);
            return View(bill.ToList());
        }

        // GET: bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = db.bill.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: bills/Create
        public ActionResult Create()
        {
            ViewBag.client_id = new SelectList(db.client, "client_id", "client_name");
            return View();
        }

        // POST: bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bill_id,client_id,date_order,descriptions")] bill bill)
        {
            if (ModelState.IsValid)
            {
                db.bill.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.client_id = new SelectList(db.client, "client_id", "client_name", bill.client_id);
            return View(bill);
        }

        // GET: bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = db.bill.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.client_id = new SelectList(db.client, "client_id", "client_name", bill.client_id);
            return View(bill);
        }

        // POST: bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bill_id,client_id,date_order,descriptions")] bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.client_id = new SelectList(db.client, "client_id", "client_name", bill.client_id);
            return View(bill);
        }

        // GET: bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = db.bill.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill bill = db.bill.Find(id);
            db.bill.Remove(bill);
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
