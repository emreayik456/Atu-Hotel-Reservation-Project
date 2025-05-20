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
    public class RoomController : Controller
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
            var roomsforavailability = db.Room.ToList();
            foreach (var room in roomsforavailability)
            {
                if(db.Reservation.SingleOrDefault(r => r.RoomID == room.id) == null)
                {
                    room.AvailabilityID = 2;
                }
            }
            db.SaveChanges();
            var rooms = db.Room.OrderBy(u => u.RoomTypeID).ToList();
            var reservations = db.Reservation.ToList();
            var viewModel = new RoomReservationViewModel
            {
                Rooms = rooms,
                Reservations = reservations
            };

            return View(viewModel);

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

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string floor_no, string AreaID, string RoomTypeID)
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

            Room room = new Room();

            room.floor_no = Convert.ToInt32(floor_no);
            room.AvailabilityID = 2;
            room.AreaID = Convert.ToInt32(AreaID);
            room.RoomTypeID = Convert.ToInt32(RoomTypeID);
            db.Room.Add(room);
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
            Room room = db.Room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            if (room.AvailabilityID == 2)
            {
                return View(room);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string floor_no, string AreaID, string RoomTypeID)
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
            Room room = db.Room.Find(id);
            if (room.AvailabilityID == 2)
            {
                room.floor_no = Convert.ToInt32(floor_no);
                room.AreaID = Convert.ToInt32(AreaID);
                room.RoomTypeID = Convert.ToInt32(RoomTypeID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


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
            Room room = db.Room.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string floor_no)
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
            Room room = db.Room.Find(id);
            if (room.AvailabilityID == 2)
            {
                db.Room.Remove(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }


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
