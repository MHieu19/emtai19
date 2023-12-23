using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPhoneMobile.Models;

namespace WebPhoneMobile.Models
{
    public class cartItem
    {
        public product shopping_product { get; set; }
        public int shopping_quanlity { get; set; }
    }
    public class Cart
    {
        List<cartItem> items = new List<cartItem>();

        public IEnumerable<cartItem> Items
        {
            get { return items; }
        }
        public void Add(product product, int quanlity = 1)
        {
            var item = items.FirstOrDefault(s => s.shopping_product.product_id == product.product_id);
            if (item == null)
            {
                items.Add(new cartItem
                {
                    shopping_product = product,
                    shopping_quanlity = quanlity
                });
            }
            else
            {
                item.shopping_quanlity += quanlity;
            }
        }
        public void Update(string product, int quanlity)
        {
            var item = items.Find(s => s.shopping_product.product_id == product);
            if (item != null)
            {
                item.shopping_quanlity = quanlity;
            }
        }

        //internal void Update(int product_id, int quanlity)
        //{
        //    throw new NotImplementedException();
        //}
        public double Total()
        {
            var total = items.Sum(s => s.shopping_product.price * s.shopping_quanlity);
            return (double)total;
        }
        public void Delete_CartItem(string product_id)
        {
            items.RemoveAll(s => s.shopping_product.product_id == product_id);
        }
        //tong so luong mua 
        public int Total_Quanlity()
        {
            return items.Sum(s => s.shopping_quanlity);
        }
        //xoa gio hang de thuc hien gio hang moi
        public void Clear_Cart()
        {
            items.Clear();
        }
    }
}