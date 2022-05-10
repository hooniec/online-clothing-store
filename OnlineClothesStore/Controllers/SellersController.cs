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
            if (ModelState.IsValid)
            {
                var obj = db.Sellers.Where(s => s.Username.Equals(seller.Username) && s.Password.Equals(seller.Password)).FirstOrDefault();
                if (obj != null)
                {
                    // Put Seller's details into Session 
                    Session["SellerId"] = obj.SId.ToString();
                    Session["UserName"] = obj.Username.ToString();
                    Session["LoginStatus"] = 1;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("LoginFail");
        }

        public ActionResult Logout()
        {
            Session["SellerId"] = null;
            return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SId,Name,Address,Phone,Email,Username,Password")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(seller);
                db.SaveChanges();
                return RedirectToAction("Login");
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
                db.Entry(seller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
