using SoftDrunk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftDrunk.Controllers
{
    public class HomeController : Controller
    {
        private Soft_DrunkEntities db = new Soft_DrunkEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PhoneProduct(Product product)
        {
            var products = db.Products.Where(p => p.Category.CategoryName == "Iphone").Take(4).ToList();
            return PartialView(products);
        }
        public ActionResult LaptopProduct(Product product)
        {
            var products = db.Products.Where(p => p.Category.CategoryName == "Macbook").Take(4).ToList();
            return PartialView(products);
        }
        public ActionResult IpadProduct(Product product)
        {
            var products = db.Products.Where(p => p.Category.CategoryName == "Ipad").Take(4).ToList();
            return PartialView(products);
        }

    }
}