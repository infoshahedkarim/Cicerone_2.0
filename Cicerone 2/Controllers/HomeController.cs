using Cicerone_2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cicerone_2.Controllers
{
    public class HomeController : Controller
    {
        private cicerone5Entities db = new cicerone5Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            return View();
        }

        public ActionResult Service()
        {


            return View();
        }

        public ActionResult Contact()
        {


            return View();
        }
        public ActionResult Package()
        {


            return View();
        }
        public ActionResult Payment()
        {


            return View();
        }
        [HttpGet]
        public ActionResult Booking()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Package(tour xvm)
        {
            tour t = new tour();
            t.tour_username = xvm.tour_username;
            t.tour_usermail = xvm.tour_usermail;
            t.tour_date = xvm.tour_date;
            t.total_destination = xvm.total_destination;
            t.tour_details = xvm.tour_details;
            db.tours.Add(t);

            try
            {
                db.SaveChanges();
                return RedirectToAction("Payment","Home");
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
           
        }

        [HttpPost]
        public ActionResult Payment(payment pvm)
        {
            payment p = new payment();
            p.payable = pvm.payable;
            p.due = pvm.due;
            p.trans_id = pvm.trans_id;
            p.payee_Mail = pvm.payee_Mail;
            db.payments.Add(p);

            try
            {
                db.SaveChanges();
                ViewBag.Message = "Confirmation mail will sent your email.";
                return RedirectToAction("Index", "Home");
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

        }
    }
}