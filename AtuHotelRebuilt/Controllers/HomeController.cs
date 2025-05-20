using AtuHotelRebuilt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtuHotelRebuilt.Controllers
{
    public class HomeController : Controller
    {
        PROJEEntities db = new PROJEEntities();
        public ActionResult Reservation()
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 3)
                {
                    ViewBag.Error = "Bu sayfaya giremezsiniz";
                    return RedirectToAction("Error", "Home");
                }
            }
            var usersessiondata = Session["TCode"].ToString();

            var reservations = db.Reservation.Where(r => r.CustomerTC == usersessiondata);
            return View(reservations.ToList());
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}