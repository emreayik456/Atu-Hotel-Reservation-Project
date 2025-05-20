using AtuHotelRebuilt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtuHotelRebuilt.Controllers
{
    public class CustomerController : Controller
    {
        PROJEEntities db = new PROJEEntities();
        // GET: Customer
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
                Customer cus = db.Customer.Find(item.CustomerTC);
                cus.is_active = true;

            }

            db.SaveChanges();
            return View(db.Customer.ToList());


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
        public ActionResult Create(string fname, string lname, DateTime birthdate, string phone, string tcNumber, string email, string gender, string linkvalue)
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
            Customer temp = db.Customer.Find(tcNumber);
            if (temp != null)
            {
                Session["CustomerTC"] = tcNumber;
                return RedirectToAction("Create", "Reservation");
            }
            else
            {
                Customer customer = new Customer();
                customer.birthdate = birthdate;
                customer.phone = phone;
                customer.tcNumber = tcNumber;
                customer.email = email;
                customer.fname = fname;
                customer.lname = lname;

                if (gender == "Male")
                {
                    customer.gender = false;
                }

                else if (gender == "Female")
                {
                    customer.gender = true;
                }

                db.Customer.Add(customer);
                db.SaveChanges();

                if (linkvalue == "1")
                {
                    return RedirectToAction("Index");
                }
                else if (linkvalue == "2")
                {
                    Session["CustomerTC"] = tcNumber;
                    return RedirectToAction("Create", "Reservation");
                }

                return RedirectToAction("Edit");
            }

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
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            var customer = db.Customer.Find(tcNumber);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(string fname, string lname, DateTime birthdate, string phone, string tcNumber, string email, string gender)
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
            if (tcNumber == null)
            {
                return RedirectToAction("Signup", "Login");
            }

            Customer customer = db.Customer.Find(tcNumber);
            customer.birthdate = birthdate;
            customer.phone = phone;
            customer.email = email;
            customer.fname = fname;
            customer.lname = lname;

            if (gender == "Male")
            {
                customer.gender = false;
            }

            else if (gender == "Female")
            {
                customer.gender = true;
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
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Customer customer = db.Customer.Find(tcNumber);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
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
                if (sessiondata == 3)
                {
                    ViewBag.Error = "Erişim Yetkiniz Yok";
                    return RedirectToAction("Error", "Home");
                }
            }
            Customer customer = db.Customer.Find(tcNumber);
            var user = db.User.SingleOrDefault(u => u.CustomerTC == customer.tcNumber);
            if (user == null)
            {
                db.Customer.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.User.Remove(user);
                db.Customer.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


    }
}