using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineClothesStore.Models;

namespace OnlineClothesStore.Controllers
{
    public class ClothesController : Controller
    {
        private StoreDatabaseEntities db = new StoreDatabaseEntities();

        // GET: Clothes
        public ActionResult Index()
        {
            return View(db.Clothes.ToList());
        }

        // GET: Clothes/Search?term
        public ActionResult Search(string term)
        {
            var searchResult = db.Clothes.AsQueryable();

            if (!String.IsNullOrEmpty(term))
            {
                var terms = term.Trim().Split(' ');

                foreach (var word in terms)
                {
                    searchResult = searchResult
                   .Where(c => c.Gender.Equals(word)
                            || c.Category.Contains(word)
                            || c.Condition.Contains(word)
                            || c.Color.Contains(word)
                            || c.Brand.Contains(word)
                            || c.Location.Contains(word));
                }
            }

            ViewBag.Keyword = term;
            ViewBag.Count = searchResult.Count();

            return View(searchResult);
        }

        // Get: Clothes/ClothingByCategory?keyword
        public ActionResult ClothingByCategory(string keyword)
        {
            var list = db.Clothes.AsQueryable();

            if(keyword != null)
            {
                list = list.Where(c => c.Condition.Equals(keyword) 
                                    || c.Gender.Equals(keyword) 
                                    || c.Category.Equals(keyword));
            }

            ViewBag.Keyword = keyword;
            ViewBag.Count = list.Count();

            return View(list);
        }

        // GET: Clothes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // GET: Clothes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clothes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CId,Gender,Category,Condition,Color,Size,Brand,Location,Price,Image")] Cloth cloth, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    try
                    {
                        string fileName = DateTime.Now.ToString("yyyymmddhhmmssfff");
                        string extension = Path.GetExtension(Image.FileName);
                        string path = Path.Combine(Server.MapPath("~/Images"), fileName + extension);
                        Cloth newCloth = new Cloth
                        {
                            Gender = cloth.Gender,
                            Category = cloth.Category,
                            Condition = cloth.Condition,
                            Color = cloth.Color,
                            Size = cloth.Size,
                            Brand = cloth.Brand,
                            Location = cloth.Location,
                            Price = cloth.Price,
                            Image = fileName + extension,
                        };

                        db.Clothes.Add(newCloth);
                        db.SaveChanges();
                        Image.SaveAs(path);

                    }
                    catch
                    {
                        return View("ClothesFail");
                    }
                }
                else
                {
                    Cloth newCloth = new Cloth
                    {
                        Gender = cloth.Gender,
                        Category = cloth.Category,
                        Condition = cloth.Condition,
                        Color = cloth.Color,
                        Size = cloth.Size,
                        Brand = cloth.Brand,
                        Location = cloth.Location,
                        Price = cloth.Price,
                        Image = "default.jpg",
                    };

                    db.Clothes.Add(newCloth);
                    db.SaveChanges();
                }
                return Content("<script language='javascript' type='text/javascript'>alert('Your product has been successfully added.');window.location.href='/Clothes/Index';</script>");
            }
            return View(cloth);
        }

        // GET: Clothes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // POST: Clothes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CId,Gender,Category,Condition,Color,Size,Brand,Location,Price")] Cloth cloth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cloth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cloth);
        }

        // GET: Clothes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloth cloth = db.Clothes.Find(id);
            if (cloth == null)
            {
                return HttpNotFound();
            }
            return View(cloth);
        }

        // POST: Clothes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cloth cloth = db.Clothes.Find(id);
            db.Clothes.Remove(cloth);
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
