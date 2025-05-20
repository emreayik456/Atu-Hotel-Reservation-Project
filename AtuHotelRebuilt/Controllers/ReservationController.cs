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
    public class ReservationController : Controller
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
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }

            foreach (var item in db.Reservation)
            {
                if (item.checkout_date.Year < DateTime.Now.Year)
                {
                    item.Room.AvailabilityID = 2;
                }

            }

            db.SaveChanges();
            return View(db.Reservation.ToList());
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
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            return View(db.Room.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DateTime checkin_date, DateTime checkout_date, string CustomerTC, string roomtype, string guestnum)
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

            if (checkin_date.DayOfYear <= checkout_date.DayOfYear)
            {
                int roomtypeID = 1;
                if (roomtype == "Economic") roomtypeID = 1;
                else if (roomtype == "Cummon") roomtypeID = 2;
                else if (roomtype == "Premium") roomtypeID = 3;
                else if (roomtype == "King Suit") roomtypeID = 4;
                Reservation reservation = new Reservation();
                int roomsize = (Convert.ToInt32(guestnum) + 1);
                Room room = db.Room.FirstOrDefault(u => u.RoomTypeID == roomtypeID && u.AvailabilityID == 2 && u.AreaID == roomsize);
                if (room != null)
                {
                    reservation.RoomID = room.id;
                    reservation.checkin_date = checkin_date;
                    reservation.checkout_date = checkout_date;
                    reservation.CustomerTC = CustomerTC;
                    room.AvailabilityID = 3;
                    RoomArea roomarea = db.RoomArea.Find(room.AreaID);
                    int cost = (checkout_date.DayOfYear - checkin_date.DayOfYear + 1) * roomarea.cost;
                    reservation.cost = cost;
                    db.Reservation.Add(reservation);
                    db.SaveChanges();
                    Session["ReservationId"] = reservation.id;
                    if ((Convert.ToInt32(guestnum) + 1) == 1)
                    {
                        return RedirectToAction("Index");
                    }
                    else if ((Convert.ToInt32(guestnum) + 1) == 2)
                    {
                        return RedirectToAction("Create1", "Guest");
                    }
                    else if ((Convert.ToInt32(guestnum) + 1) == 3)
                    {
                        return RedirectToAction("Create2", "Guest");
                    }
                    else if ((Convert.ToInt32(guestnum) + 1) == 4)
                    {
                        return RedirectToAction("Create3", "Guest");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                }
                else
                {
                    Reservation[] reservations = db.Reservation.ToArray();
                    for (int i = reservations.Length; i >= 0; i--)
                    {                                                                                                                                                                                                               //| = checkin_date  and { = reservations[i].checkin_date
                        if (!((reservations[i].checkin_date < checkin_date && reservations[i].checkout_date > checkout_date) ||                                                                                                     // { | | }    
                            (reservations[i].checkin_date < checkin_date && (reservations[i].checkout_date < checkout_date && reservations[i].checkout_date > checkin_date)) ||                                                     // { | } |
                            ((reservations[i].checkin_date > checkin_date && reservations[i].checkin_date < checkout_date) && reservations[i].checkout_date > checkout_date) ||                                                     // | { | }
                            ((reservations[i].checkin_date > checkin_date && reservations[i].checkin_date < checkout_date) && (reservations[i].checkout_date < checkout_date && reservations[i].checkout_date > checkin_date))))    // | { } |
                        {
                            Room room2 = db.Room.Find(reservations[i].RoomID);
                            if (room2.RoomTypeID == roomtypeID && room2.AreaID == (Convert.ToInt32(guestnum) + 1))
                            {
                                reservation.RoomID = room2.id;
                                reservation.checkin_date = checkin_date;
                                reservation.checkout_date = checkout_date;
                                reservation.CustomerTC = CustomerTC;
                                room.AvailabilityID = 3;
                                RoomArea roomarea = db.RoomArea.Find(room2.AreaID);
                                int cost = (checkout_date.DayOfYear - checkin_date.DayOfYear + 1) * roomarea.cost;
                                reservation.cost = cost;
                                db.Reservation.Add(reservation);
                                db.SaveChanges();
                                Session["ReservationId"] = reservation.id;
                                if ((Convert.ToInt32(guestnum) + 1) == 1)
                                {
                                    return RedirectToAction("Index");
                                }
                                else if ((Convert.ToInt32(guestnum) + 1) == 2)
                                {
                                    return RedirectToAction("Create1", "Guest");
                                }
                                else if ((Convert.ToInt32(guestnum) + 1) == 3)
                                {
                                    return RedirectToAction("Create2", "Guest");
                                }
                                else if ((Convert.ToInt32(guestnum) + 1) == 4)
                                {
                                    return RedirectToAction("Create3", "Guest");
                                }
                                else
                                {
                                    return RedirectToAction("Index");
                                }
                            }
                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }

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
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }

            return View(reservation);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DateTime checkin_date, DateTime checkout_date, string CustomerTC, string roomtype, int id)
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
            if (checkin_date.DayOfYear <= checkout_date.DayOfYear)
            {
                int roomtypeID = 1;
                if (roomtype == "Economic") roomtypeID = 1;
                else if (roomtype == "Cummon") roomtypeID = 2;
                else if (roomtype == "Premium") roomtypeID = 3;
                else if (roomtype == "King Suit") roomtypeID = 4;
                Reservation reservation = db.Reservation.Find(id);
                Room room = db.Room.First(u => u.RoomTypeID == roomtypeID && u.AvailabilityID == 2);
                if (room != null)
                {
                    reservation.RoomID = room.id;
                    reservation.checkin_date = checkin_date;
                    reservation.checkout_date = checkout_date;
                    reservation.CustomerTC = CustomerTC;
                    room.AvailabilityID = 3;
                    RoomArea roomarea = db.RoomArea.Find(room.AreaID);
                    int cost = (checkout_date.DayOfYear - checkin_date.DayOfYear + 1) * roomarea.cost;
                    reservation.cost = cost;
                    db.SaveChanges();
                }
                else
                {
                    Reservation[] reservations = db.Reservation.ToArray();
                    for (int i = reservations.Length; i >= 0; i--)
                    {                                                                                                                                                                                                               //| = checkin_date  and { = reservations[i].checkin_date
                        if (!((reservations[i].checkin_date < checkin_date && reservations[i].checkout_date > checkout_date) ||                                                                                                     // { | | }    
                            (reservations[i].checkin_date < checkin_date && (reservations[i].checkout_date < checkout_date && reservations[i].checkout_date > checkin_date)) ||                                                     // { | } |
                            ((reservations[i].checkin_date > checkin_date && reservations[i].checkin_date < checkout_date) && reservations[i].checkout_date > checkout_date) ||                                                     // | { | }
                            ((reservations[i].checkin_date > checkin_date && reservations[i].checkin_date < checkout_date) && (reservations[i].checkout_date < checkout_date && reservations[i].checkout_date > checkin_date))))    // | { } |
                        {
                            Room room2 = db.Room.Find(reservations[i].RoomID);
                            if (room2.RoomTypeID == roomtypeID)
                            {
                                reservation.RoomID = room2.id;
                                reservation.checkin_date = checkin_date;
                                reservation.checkout_date = checkout_date;
                                reservation.CustomerTC = CustomerTC;
                                room.AvailabilityID = 3;
                                RoomArea roomarea = db.RoomArea.Find(room2.AreaID);
                                int cost = (checkout_date.DayOfYear - checkin_date.DayOfYear + 1) * roomarea.cost;
                                reservation.cost = cost;
                                db.SaveChanges();
                                break;
                            }
                        }
                    }
                }


            }

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
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id, DateTime checkin_date)
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
            Reservation reservation = db.Reservation.Find(id);
            Guest guest = db.Guest.FirstOrDefault(u => u.ReservationID == id);
            if (guest != null)
            {
                return RedirectToAction("Index");
            }
            db.Reservation.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
