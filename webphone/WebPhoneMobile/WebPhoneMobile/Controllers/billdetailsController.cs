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
    public class billdetailsController : Controller
    {
        private WebPhoneMobileEntities db = new WebPhoneMobileEntities();

        // GET: billdetails
        public ActionResult Index(int bill_id)
        {
            using (WebPhoneMobileEntities db = new WebPhoneMobileEntities())
            {
                List<client> client = db.client.ToList();
                List<bill> bill = db.bill.ToList();
                List<product> product = db.product.ToList();
                List<billdetail> billdetail = db.billdetail.ToList();
                var main = from h in bill
                           join k in client on h.client_id equals k.client_id
                           where (h.bill_id == bill_id)
                           select new ViewModel
                           {
                               bill = h,
                               client = k
                           };
                var sub = from c in billdetail
                          join s in product on c.product_id equals s.product_id
                          where (c.bill_id == bill_id)
                          select new ViewModel
                          {
                              billdetail = c,
                              product = s,
                              amount = Convert.ToDouble(c.price * c.quanlity)
                          };
                // truyen hai doi tuong trên sang View
                ViewBag.Main = main;
                ViewBag.Sub = sub;
                return View();
            }
        }

        // GET: billdetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billdetail billdetail = db.billdetail.Find(id);
            if (billdetail == null)
            {
                return HttpNotFound();
            }
            return View(billdetail);
        }

        // GET: billdetails/Create
        public ActionResult Create()
        {
            ViewBag.bill_id = new SelectList(db.bill, "bill_id", "client_id");
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name");
            return View();
        }

        // POST: billdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bill_id,product_id,quanlity,price")] billdetail billdetail)
        {
            if (ModelState.IsValid)
            {
                db.billdetail.Add(billdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.bill_id = new SelectList(db.bill, "bill_id", "client_id", billdetail.bill_id);
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name", billdetail.product_id);
            return View(billdetail);
        }

        // GET: billdetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billdetail billdetail = db.billdetail.Find(id);
            if (billdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.bill_id = new SelectList(db.bill, "bill_id", "client_id", billdetail.bill_id);
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name", billdetail.product_id);
            return View(billdetail);
        }

        // POST: billdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bill_id,product_id,quanlity,price")] billdetail billdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.bill_id = new SelectList(db.bill, "bill_id", "client_id", billdetail.bill_id);
            ViewBag.product_id = new SelectList(db.product, "product_id", "product_name", billdetail.product_id);
            return View(billdetail);
        }

        // GET: billdetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            billdetail billdetail = db.billdetail.Find(id);
            if (billdetail == null)
            {
                return HttpNotFound();
            }
            return View(billdetail);
        }

        // POST: billdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            billdetail billdetail = db.billdetail.Find(id);
            db.billdetail.Remove(billdetail);
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
