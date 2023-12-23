using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPhoneMobile.Models;
using PagedList;

namespace WebPhoneMobile.Controllers
{
    public class productsController : Controller
    {
        private WebPhoneMobileEntities db = new WebPhoneMobileEntities();

        // GET: products
        public ActionResult Index(string sortOrder, int? page)
        {
            // nếu sortOrder rỗng thì đặt SortByName là "ten_desc", ngược lại là rỗng
            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            // nếu sortOrder bằng "dongia" thì SortByPrice là "dongia_desc", ngược lại là "dongia"
            ViewBag.SortByPrice = (sortOrder == "price" ? "price_desc" : "price");
            ViewBag.CurrentSort = sortOrder;
            // lấy các liên kết
            var product = db.product.Include(s => s.category);
            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.category);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.price);
                    break;
                case "price":
                    product = product.OrderBy(s => s.price);
                    break;
                default:
                    // mặc định sắp tăng dần theo tên sản phẩm
                    product = product.OrderBy(s => s.product_name);
                    break;
            }
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }


        // GET: products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.category, "category_id", "category_name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,category_id,price,product_detail,product_image")] product product, HttpPostedFileBase product_image)
        {
            if (ModelState.IsValid)
            {
                if (product_image != null && product_image.ContentLength > 0)
                {
                    string filename = Path.GetFileName(product_image.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    product.product_image = "Images/" + filename;
                    product_image.SaveAs(path);
                }
                db.product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.category, "category_id", "category_name", product.category_id);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.category, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,category_id,price,product_detail,product_image")] product product, HttpPostedFileBase imageUpload, string product_image)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null && imageUpload.ContentLength > 0)
                {
                    string filename = Path.GetFileName(imageUpload.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    product.product_image = "Images/" + filename;
                    imageUpload.SaveAs(path);
                }
                else
                {
                    product.product_image = product_image;
                    //nếu không chọn hình mới thì giữ hình cũ
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.category, "category_id", "category_name", product.category_id);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        // GET: products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            product product = db.product.Find(id);
            db.product.Remove(product);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath("~/" + product.product_image));
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
