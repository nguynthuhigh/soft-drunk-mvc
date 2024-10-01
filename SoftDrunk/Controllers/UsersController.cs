using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoftDrunk.Models;

namespace SoftDrunk.Controllers
{
    public class UsersController : Controller
    {
        private Soft_DrunkEntities db = new Soft_DrunkEntities();


        // GET: Users/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                User foundUser = db.Users.Where(s => s.Username == user.Username).FirstOrDefault();
                if (foundUser != null)
                {
                    ViewBag.AccountExists = "Tài khoản đã tồn tại";
                    return View();

                }

                user.UserRole = "user";
                db.Users.Add(user);
                db.SaveChanges();
                Session["Customer"] = user;
                return RedirectToAction("Index", "Home");
            }
            catch (DbEntityValidationException ex)
            {
                ViewBag.Error = "Có lỗi xảy ra trong quá trình đăng ký. Vui lòng thử lại.";
                return View();
            }
        }


        // GET: Users/Login
        public ActionResult Login()
        {
            return View();
        }
        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User foundUser = db.Users.Where(s=>s.Password == user.Password && s.Username == user.Username).FirstOrDefault();
                if(foundUser == null)
                {
                    ViewBag.NotFoundUser = "Tài khoản hoặc mật khẩu sai";
                    return View();
                }
                Session["Customer"] = foundUser;
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }
        // GET: Users/Login
        public ActionResult NewInfo()
        {
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewInfo(Customer customer)
        {
            if (Session["Customer"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            User user = Session["Customer"] as User;
            if (ModelState.IsValid)
            {
                customer.Username = user.Username;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(customer);
        }
    }
}
