using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTS.Models;

namespace DTS.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        /// <summary>
        ///  Categories Actions
        ///  written by Semih Elitas
        /// </summary>

        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext db = new DatabaseContext();


        #endregion

        #region List Method

        // GET: Categories
        public ActionResult List()
        {
            TempData.Keep("currentRole");
            var categories = db.Category.Include(y => y.Product);
            return View(categories.ToList());
        }

        #endregion

        #region Details Method
        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        #endregion

        #region Add Methods
        // GET: Categories/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: Categories/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Category.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bu kategori kaydı zaten mevcut, lütfen tekrar deneyin!");
                }
            }
            return View(category);
        }

        #endregion

        #region Update Methods
        // GET: Categories/Update/5
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(category);
        }

        #endregion

        #region Delete Methods
        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
