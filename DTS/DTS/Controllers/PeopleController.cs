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
    public class PeopleController : Controller
    {
        /// <summary>
        ///  People(Employee) Actions
        ///  written by Semih Elitas
        /// </summary>

        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext db = new DatabaseContext();


        #endregion

        #region List Method
        // GET: People
        public ActionResult List()
        {
            TempData.Keep("currentRole");
            var people = db.Person.Include(y => y.Product);
            return View(people.ToList());
        }
        #endregion

        #region Details Method
        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        #endregion

        #region Add Methods
        // GET: People/Add
        public ActionResult Add()
        {
            return View();
        }

        // POST: People/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "PersonID,PersonName,PersonContact")] Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Person.Add(person);
                    db.SaveChanges();
                    return RedirectToAction("List");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Bu personel kaydı zaten mevcut, lütfen tekrar deneyin!");
                }
            }

            return View(person);
        }
        #endregion

        #region Update Methods
        // GET: People/Update/5
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Update/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "PersonID,PersonName,PersonContact")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(person);
        }
        #endregion

        #region Delete Methods
        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
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
