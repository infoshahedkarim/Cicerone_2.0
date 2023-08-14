using Cicerone_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Data.Entity.Validation;

namespace Cicerone_2.Controllers
{
    public class UserController : Controller
    {
        private cicerone5Entities db = new cicerone5Entities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(db_user uvm)
        {
            
                db_user u = new db_user();
                u.username = uvm.username;
                u.user_mail = uvm.user_mail;
                u.user_pass = uvm.user_pass;
                u.user_contact = uvm.user_contact;
                u.user_nid = uvm.user_nid;
                db.db_user.Add(u);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        
                        ViewBag.error = "invalid iputs or blank data";
                    }
                }
            }
            return View();

        } //method......................... end.....................

        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult login(db_user avm)
        {
            db_user us = db.db_user.Where(x => x.user_mail == avm.user_mail && x.user_pass == avm.user_pass).SingleOrDefault();
            if (us != null)
            {

                Session["useer_id"] = us.useer_id.ToString();
                Session["username"] = us.username.ToString();
                return RedirectToAction("Index","Home");

            }
            else
            {
                ViewBag.error = "Invalid mail or password";

            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("login", "User");
        }
    }
}