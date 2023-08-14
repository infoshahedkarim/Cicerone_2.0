using Cicerone_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Cicerone_2.Controllers
{
    public class AdminController : Controller
    {
        private cicerone5Entities db = new cicerone5Entities();
        // GET: Admin
        [HttpGet]
        public ActionResult Administrative()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Administrative(db_admin avm)
        {
            db_admin admin = db.db_admin.Where(x => x.admin_username == avm.admin_username && x.admin_password == avm.admin_password).SingleOrDefault();
            if (admin != null)
            {
                Session["admin_id"] = admin.admin_id.ToString();
                Session["admin_username"] = admin.admin_username.ToString();
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.error = "Invalid Username or Password";
            }
            return View();

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult create()
        {
            if (Session["admin_id"] == null)
            {
                return RedirectToAction("Administrative");
            }
            return View();
        }
        [HttpPost]
        public ActionResult create(package pvm, HttpPostedFileBase imgfile)
        {
            {
                string path = uploadimgfile(imgfile);
                if (path.Equals("-1"))
                {
                    ViewBag.error = "Image could not be uploaded....";
                }
                else
                {
                    package pak = new package();
                    pak.package_name = pvm.package_name;
                    pak.package_details = pvm.package_details;
                    pak.package_image = path;
                    pak.admin_id = Convert.ToInt32(Session["admin_id"].ToString());
                    pak.package_source = pvm.package_source;
                    db.packages.Add(pak);
                    db.SaveChanges();
                    return RedirectToAction("viewPackage","Admin");
                }

                return View();
            } //end,,,,,,,,,,,,,,,,,,,

        }


        public ActionResult ViewPackage(int?page)
        {
            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.packages.OrderByDescending(x => x.package_id).ToList();
            IPagedList<package> ptu = list.ToPagedList(pageindex, pagesize);


            return View(ptu);


        }
       


        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }

            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }



            return path;
        }


        public ActionResult BookingList(int? page)
        {

            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.tours.OrderByDescending(x => x.tour_id).ToList();
            IPagedList<tour> ptu = list.ToPagedList(pageindex, pagesize);


            return View(ptu);


        }

        public ActionResult paymentsData(int? page)
        {

            int pagesize = 9, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.payments.OrderByDescending(x => x.payment_id).ToList();
            IPagedList<payment> ptu = list.ToPagedList(pageindex, pagesize);


            return View(ptu);


        }


    }
}