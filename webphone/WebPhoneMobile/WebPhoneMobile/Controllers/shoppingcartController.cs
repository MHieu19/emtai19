using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPhoneMobile.Models;
using Microsoft.Ajax.Utilities;

namespace WebPhoneMobile.Controllers
{
    public class shoppingcartController : Controller
    {
        WebPhoneMobileEntities db = new WebPhoneMobileEntities();
        // GET: shoppingcart
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(string product_id)
        {
            var product = db.product.SingleOrDefault(s => s.product_id == product_id);
            if (product != null)
            {
                GetCart().Add(product);
            }
            return RedirectToAction("showToCart", "shoppingcart");
        }
        // page view
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("showToCart", "shoppingcart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult Update(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            string product_id = form["product_id"];
            int quanlity = int.Parse(form["quanlity"]);
            cart.Update(product_id, quanlity);
            return RedirectToAction("showToCart", "shoppingcart");
        }
        public ActionResult Delete(string product_id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Delete_CartItem(product_id);
            return RedirectToAction("showToCart", "shoppingcart");
        }
        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_item = cart.Total_Quanlity();
            ViewBag.QuanlityCart = total_item;
            return PartialView("BagCart");
        }
        public ActionResult shopping_success()
        {
            return View();
        }
        public ActionResult Order(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                bill bill = new bill();
                bill.date_order = DateTime.Now;  
                bill.client_id = form["client_id"];
                bill.descriptions = form["descriptions"];
                db.bill.Add(bill);
                foreach (var item in cart.Items)
                {
                    billdetail bill_detail = new billdetail();
                    bill_detail.bill_id = bill.bill_id;
                    bill_detail.product_id = item.shopping_product.product_id;
                    bill_detail.price = item.shopping_product.price;
                    bill_detail.quanlity = item.shopping_quanlity;
                    db.billdetail.Add(bill_detail);
                }
                db.SaveChanges();
                cart.Clear_Cart();
                return RedirectToAction("shopping_success", "shoppingcart");

            }
            catch
            {
                return Content("error order. Please information of client");
            }
        }
    }
}