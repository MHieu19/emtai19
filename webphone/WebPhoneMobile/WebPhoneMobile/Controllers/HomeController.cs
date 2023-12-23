using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WebPhoneMobile.Models;
using PagedList;

namespace WebPhoneMobile.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private WebPhoneMobileEntities db = new WebPhoneMobileEntities();
        public ActionResult Index(string currentFilter, int? page, int category_id = 0, string searchString = "")
        {
            //var product = db.products.Include(s => s.category);
            //return View(product.ToList());
            if (searchString != "")
            {
                page = 1;
                var products = db.product.Include(s => s.category).Where(x => x.product_name.ToUpper().Contains(searchString.ToUpper()));
                products = products.OrderBy(x => x.product_name);
                int pageSize = products.Count();
                int pageNumber = (page ?? 1);
                return View(products.ToPagedList(pageNumber, pageSize));

                //var products = db.products.Include(s => s.category).Where(x =>x.product_name.ToLower().Contains(searchString.ToLower()));
                //return View(products.ToList());
            }
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = currentFilter;
            if (category_id == 0)
            {
                int pageSize = 9;
                int pageNumber = (page ?? 1);
                var products = db.product.Include(s => s.category).OrderBy(x => x.product_name);
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            else // lọc theo loại sản phẩm
            {
                var products = db.product.Include(s => s.category).Where(x => x.category_id == category_id);
                products = products.OrderBy(x => x.product_name);
                int pageSize = products.Count();
                int pageNumber = (page ?? 1);
                return View(products.ToPagedList(pageNumber, pageSize));
            }
            //if (category_id == 0)
            //{
            //    var product = db.products.Include(s => s.category);
            //    return View(product.ToList());
            //}
            //else
            //{
            //    var product = db.products.Include(s => s.category).Where(x => x.category_id == category_id);
            //    return View(product.ToList());
            //}
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}