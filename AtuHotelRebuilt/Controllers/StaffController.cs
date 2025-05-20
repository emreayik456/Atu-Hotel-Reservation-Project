using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AtuHotelRebuilt.Models;

namespace AtuHotelRebuilt.Controllers
{
    public class StaffController : Controller
    {
        private PROJEEntities db = new PROJEEntities();


        public ActionResult Index()
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(db.Staff.ToList());
        }


        public ActionResult Create()
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string fname, string lname, DateTime birthdate, string phone, string tcNumber, string email, string gender, string DepartmentID, string dayoff, string address)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Staff staff = new Staff();
            staff.fname = fname;
            staff.lname = lname;
            staff.birthdate = birthdate;
            staff.phone = phone;
            staff.tcNumber = tcNumber;
            staff.email = email;
            staff.DepartmentID = Convert.ToInt32(DepartmentID);
            staff.dayoff = dayoff;
            staff.address = address;
            if (gender == "Male")
            {
                staff.gender = false;
            }
            else
            {
                staff.gender = true;
            }
            db.Staff.Add(staff);
            db.SaveChanges();
            return View(staff);
        }


        public ActionResult Edit(string tcNumber)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (tcNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(tcNumber);
            if (staff == null)
            {
                return HttpNotFound();
            }

            return View(staff);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string fname, string lname, DateTime birthdate, string phone, string tcNumber, string email, string gender, string DepartmentID, string dayoff, string address)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Staff staff = db.Staff.Find(tcNumber);
            staff.fname = fname;
            staff.lname = lname;
            staff.birthdate = birthdate;
            staff.phone = phone;
            staff.tcNumber = tcNumber;
            staff.email = email;
            staff.DepartmentID = Convert.ToInt32(DepartmentID);
            staff.dayoff = dayoff;
            staff.address = address;
            if (gender == "Male")
            {
                staff.gender = false;
            }
            else
            {
                staff.gender = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(string tcNumber)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (tcNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staff.Find(tcNumber);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string tcNumber, string fname)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata != 2)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Staff staff = db.Staff.Find(tcNumber);
            db.Staff.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
