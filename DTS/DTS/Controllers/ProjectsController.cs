using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DTS.Models;

namespace DTS.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        /// <summary>
        /// Projects Actions
        /// written by Semih Elitas
        /// </summary>
        /// 

        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext db = new DatabaseContext();


        #endregion

        #region List Method
        // GET: Projects
        [RequireHttps]
        public ActionResult List()
        {
            TempData.Keep("currentRole");
            var projects = db.Project.Include(y => y.ProjectProducts);
            return View(projects.ToList());
        }
        #endregion

        #region Add Methods
        // GET: Projects/Add
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "projectCode,projectClient,projectName, projectStartDate, projectStatus")] Project project)
        {

            if (ModelState.IsValid)
            {
                db.Project.Add(project);
                db.SaveChanges();
                return RedirectToAction("list");
            }

            return View(project);
        }
        #endregion

        #region Products Method
        public ActionResult Products(string id, string projectName)
        {
            ViewBag.ProjectName = projectName;
            ViewBag.ProjectCode = id;
            var products = db.ProjectProducts.Where(x => x.projectCode == id).ToList();
            return View(products);
        }
        #endregion

        #region Delete Methods
        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Project project = db.Project.Find(id);
            db.Project.Remove(project);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        #endregion
    }
}