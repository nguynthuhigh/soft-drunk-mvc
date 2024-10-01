using SoftDrunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftDrunk.Controllers
{
    public class CartsController : Controller
    {
        // GET: Cart
        private Soft_DrunkEntities db = new Soft_DrunkEntities();
        public ActionResult Index()
        {
            Cart cart = Session["Cart"] as Cart;
            if(cart == null || cart.CountQuantity() == 0)
            {
                return View("EmptyCart");
            }
            return View(cart);
        }
        [HttpPost]
        public ActionResult BuyNow(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            Product product = db.Products.Where(s => s.ProductID == id).FirstOrDefault();
            if (product == null)
            {
                ViewBag.ProductNotFound = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index", "Carts");
            }
            cart.AddToCart(product);
            return RedirectToAction("Index", "Carts");
        }
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            Product product = db.Products.Where(s => s.ProductID == id).FirstOrDefault();
            if(product == null)
            {
                ViewBag.ProductNotFound = "Không tìm thấy sản phẩm";
                return RedirectToAction("Index","Carts");
            }
            cart.AddToCart(product);
            return View();
        }
        [HttpPost]
        public ActionResult DecreaseProduct(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            Product product = db.Products.Where(s => s.ProductID == id).FirstOrDefault();
            cart.DecreaseProduct(product);
            return RedirectToAction("Index", "Carts");
        }


    }
}
