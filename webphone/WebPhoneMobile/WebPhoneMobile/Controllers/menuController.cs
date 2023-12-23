using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPhoneMobile.Models;

namespace WebPhoneMobile.Controllers
{
    public class menuController : Controller
    {
        // GET: menu
        public ActionResult Index()
        {
            using (WebPhoneMobileEntities db = new WebPhoneMobileEntities())
            {
                var category = db.category.ToList();
                Hashtable category_name = new Hashtable();
                foreach (var item in category)
                    category_name.Add(item.category_id, item.category_name);
                ViewBag.category_name = category_name;
                return PartialView("Index");
            }
        }
    }
}