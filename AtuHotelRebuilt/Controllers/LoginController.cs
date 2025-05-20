using AtuHotelRebuilt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AtuHotelRebuilt.Controllers
{
    public class LoginController : Controller
    {
        PROJEEntities db = new PROJEEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            string hashedpassword = ComputeSha256Hash(password);
            var user = db.User.SingleOrDefault(u => u.username == username && u.password == hashedpassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View("Login");
            }
            else if (user != null)
            {
                Session["AccessCode"] = user.UserTypeID;

                if (user.CustomerTC == null)
                {
                    Staff staff = db.Staff.Find(user.StaffTC);
                    Session["Username"] = staff.fname;
                    Session["TCode"] = staff.tcNumber;
                }
                else
                {
                    Customer customer = db.Customer.Find(user.CustomerTC);
                    Session["Username"] = customer.fname;
                    Session["TCode"] = customer.tcNumber;
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));


                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(string username, string password, string fname, string lname, DateTime birthdate,
                                    string phone, string tcNumber, string email, string gender)
        {
            var usercontrol = db.User.SingleOrDefault(u => u.username == username || u.Customer.tcNumber == tcNumber);
            if (usercontrol == null)
            {
                User user = new User();
                user.username = username;
                user.password = ComputeSha256Hash(password);
                user.UserTypeID = 3;

                Customer customer = new Customer();
                customer.fname = fname;
                customer.lname = lname;
                customer.birthdate = birthdate;
                customer.phone = phone;
                customer.tcNumber = tcNumber;
                customer.email = email;
                customer.is_active = false;

                if (gender == "Male")
                {
                    customer.gender = false;
                }

                else if (gender == "Female")
                {
                    customer.gender = true;
                }

                user.CustomerTC = customer.tcNumber;
                db.Customer.Add(customer);
                db.User.Add(user);
                db.SaveChanges();

                Session["AccessCode"] = user.UserTypeID;
                Session["Username"] = user.Customer.fname;
                Session["TCode"] = user.CustomerTC;
                return RedirectToAction("Contact", "Home");
            }
            else
            {
                ViewBag.Error = "Kullanıcı adı ya da TC numarası kayıtlı";
                return View("Signup");
            }
        }

        public ActionResult Logout()
        {
            Session["AccessCode"] = null;
            Session["Username"] = null;
            Session["TCode"] = null;
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string username, string email)
        {
            return View();
        }
    }
}