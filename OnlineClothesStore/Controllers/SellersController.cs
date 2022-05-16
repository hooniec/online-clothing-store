using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineClothesStore.Models;

namespace OnlineClothesStore.Controllers
{
    public class SellersController : Controller
    {
        private StoreDatabaseEntities db = new StoreDatabaseEntities();

        // GET: Sellers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        // The login form is loaded in this action
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        // The Login form is posted to this method
        [HttpPost]
        public ActionResult Login(Seller seller)
        {
            // Validate Username and Password of Seller model for login
            if (ModelState.IsValidField("Username") && ModelState.IsValidField("Password"))
            {
                // Look seller's details up that mathes the inputs
                var obj = db.Sellers.Where(s => s.Username.Equals(seller.Username) && s.Password.Equals(seller.Password)).FirstOrDefault();
                if (obj != null)
                {
                    // Put Seller's details into Session 
                    Session["SellerId"] = obj.SId.ToString();
                    Session["UserName"] = obj.Username.ToString();
                    // Redirect to home page with an alert message
                    return Content(string.Format("<script language='javascript' type='text/javascript'>alert('Logged in successfully as {0}!');window.location.href='/';</script>", seller.Username));
                }
                return View("LoginFail");
            }
            return View(seller);
        }

        public ActionResult Logout()
        {
            Session["SellerId"] = null;
            return Content("<script language='javascript' type='text/javascript'>alert('You have successfully logged out.');window.location.href='/';</script>");
        }

        // GET:Sellers/RequestFail
        public ActionResult LoginFail()
        {
            return View();
        }


        // GET: Sellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "SId,Name,Address,Phone,Email,Username,Password")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                //db.Sellers.Add(seller);
                //db.SaveChanges();
                return Content("<script language='javascript' type='text/javascript'>alert('Congratulations, your account has been successfully created.');window.location.href='/Sellers/Login';</script>");
            }
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SId,Name,Address,Phone,Email,Username,Password")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                // Validate Username and Password of Seller model for updating profile
                if (ModelState.IsValidField("Username") && ModelState.IsValidField("Password"))
                {
                    // Look seller's details up that mathes the inputs
                    var obj = db.Sellers.AsNoTracking().Where(s => s.Username.Equals(seller.Username) && s.Password.Equals(seller.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        // Update seller's details
                        db.Entry(seller).State = EntityState.Modified;
                        db.SaveChanges();
                        // Acknowledgement message is sent
                        return Content(string.Format("<script language='javascript' type='text/javascript'>alert('Your profile is updated successfully');window.location.href='/Sellers/Edit/{0}';</script>", seller.SId));
                    }
                    return View("AuthenticationFail");
                }
            }
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Sellers.Find(id);
            db.Sellers.Remove(seller);
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
