using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using AtuHotelRebuilt.Models;

namespace AtuHotelRebuilt.Controllers
{
    public class GuestController : Controller
    {
        private PROJEEntities db = new PROJEEntities();

        // GET: Guests
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
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            var guest = db.Guest.Include(g => g.Reservation);
            return View(guest.ToList());
        }




        // GET: Guests/Create
        public ActionResult Create1()
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            return View();
        }
        public ActionResult Create2()
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            return View();
        }
        public ActionResult Create3()
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(string fname0, string lname0, DateTime? birthdate0, string phone0, string gender0, string reservationid)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (fname0 == null || lname0 == null || birthdate0 == null || phone0 == null)
            {
                return RedirectToAction("Index");
            }

            Guest guest0 = new Guest();
            guest0.fname = fname0;
            guest0.lname = lname0;
            guest0.birthdate = birthdate0.Value;
            guest0.phone = phone0;
            if (gender0 == "Male")
            {
                guest0.gender = false;
            }
            else
            {
                guest0.gender = true;
            }
            guest0.ReservationID = Convert.ToInt32(reservationid);
            db.Guest.Add(guest0);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(string fname0, string lname0, DateTime? birthdate0, string phone0, string gender0,
                                    string fname1, string lname1, DateTime? birthdate1, string phone1, string gender1, string reservationid)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (fname0 == null || lname0 == null || birthdate0 == null || phone0 == null)
            {
                return RedirectToAction("Index");
            }

            Guest guest0 = new Guest();
            guest0.fname = fname0;
            guest0.lname = lname0;
            guest0.birthdate = birthdate0.Value;
            guest0.phone = phone0;
            if (gender0 == "Male")
            {
                guest0.gender = false;
            }
            else
            {
                guest0.gender = true;
            }
            guest0.ReservationID = Convert.ToInt32(reservationid);
            db.Guest.Add(guest0);
            db.SaveChanges();

            if (fname1 == null || lname1 == null || birthdate1 == null || phone1 == null)
            {
                return RedirectToAction("Index");
            }

            Guest guest = new Guest();
            guest.fname = fname1;
            guest.lname = lname1;
            guest.birthdate = birthdate1.Value;
            guest.phone = phone1;
            if (gender1 == "Male")
            {
                guest.gender = false;
            }
            else
            {
                guest.gender = true;
            }
            guest.ReservationID = Convert.ToInt32(reservationid);
            db.Guest.Add(guest);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create3(string fname0, string lname0, DateTime? birthdate0, string phone0, string gender0,
                                    string fname1, string lname1, DateTime? birthdate1, string phone1, string gender1,
                                    string fname2, string lname2, DateTime? birthdate2, string phone2, string gender2, string reservationid)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (fname0 == null || lname0 == null || birthdate0 == null || phone0 == null)
            {
                return RedirectToAction("Index");
            }

            Guest guest0 = new Guest();
            guest0.fname = fname0;
            guest0.lname = lname0;
            guest0.birthdate = birthdate0.Value;
            guest0.phone = phone0;
            if (gender0 == "Male")
            {
                guest0.gender = false;
            }
            else
            {
                guest0.gender = true;
            }
            guest0.ReservationID = Convert.ToInt32(reservationid);
            db.Guest.Add(guest0);
            db.SaveChanges();

            if (fname1 == null || lname1 == null || birthdate1 == null || phone1 == null)
            {
                return RedirectToAction("Index");
            }

            Guest guest = new Guest();
            guest.fname = fname1;
            guest.lname = lname1;
            guest.birthdate = birthdate1.Value;
            guest.phone = phone1;
            if (gender1 == "Male")
            {
                guest.gender = false;
            }
            else
            {
                guest.gender = true;
            }
            guest.ReservationID = Convert.ToInt32(reservationid);
            db.Guest.Add(guest);
            db.SaveChanges();

            if (fname2 == null || lname2 == null || birthdate2 == null || phone2 == null)
            {
                return RedirectToAction("Index");
            }


            Guest guest1 = new Guest();
            guest1.fname = fname2;
            guest1.lname = lname2;
            guest1.birthdate = birthdate2.Value;
            guest1.phone = phone2;
            if (gender2 == "Male")
            {
                guest1.gender = false;
            }
            else
            {
                guest1.gender = true;
            }
            guest1.ReservationID = Convert.ToInt32(reservationid);
            db.Guest.Add(guest1);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guest.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }

            return View(guest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string fname, string lname, DateTime? birthdate, string phone, string gender, int id)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Guest guest = db.Guest.Find(id);
            guest.fname = fname;
            guest.lname = lname;
            guest.birthdate = birthdate.Value;
            guest.phone = phone;
            if (gender == "Male")
            {
                guest.gender = false;
            }
            else if (gender == "Female")
            {
                guest.gender = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guest.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string fname)
        {
            var session = Session["AccessCode"];
            if (session == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int sessiondata = Convert.ToInt32(Session["AccessCode"]);
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Guest guest = db.Guest.Find(id);
            db.Guest.Remove(guest);
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
