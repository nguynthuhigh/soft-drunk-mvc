using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoftDrunk.Models;

namespace SoftDrunk.Controllers
{
    public class OrdersController : Controller
    {
        private Soft_DrunkEntities db = new Soft_DrunkEntities();

        [HttpPost]
        public ActionResult NewOrder( Order order, int CustomerID)
        {
            User user = Session["Customer"] as User;
            if (user == null)
            {
                return RedirectToAction("Login", "Users");
            }
            Cart cart = Session["Cart"] as Cart;
            Customer customer = db.Customers.Where(s => s.CustomerID == CustomerID).FirstOrDefault();
            if(customer == null)
            {
                ViewBag.Error = "Không tìm thấy thông tin khách hàng";
            }
          
            order.OrderDate = DateTime.Now;
            order.TotalAmount = cart.TotalMoney();
            order.CustomerID = CustomerID;
            order.PaymentStatus = "Đã thanh toán";
            order.AddressDelivery = customer.CustomerAddress;
            db.Orders.Add(order);
            foreach(var item in cart.Carts)
            {
                OrderDetail detail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = item.product.ProductID,
                    Quantity = item.quantity,
                    UnitPrice = item.product.ProductPrice,
                };
                db.OrderDetails.Add(detail);
            }
            db.SaveChanges();
            cart.ClearCart();
            return View("OrderSuccess");
        }
        public ActionResult OrderSuccess()
        {
            return View();
        }
    }
}
