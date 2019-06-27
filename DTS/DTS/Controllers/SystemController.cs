using DTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DTS.Controllers
{
    [Authorize]
    public class SystemController : Controller
    {
        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext db = new DatabaseContext();


        #endregion

        #region Home - Index
        public ActionResult Home()
        {
            List<Requests> requests = db.Requests.Where(x => x.requestUsername == db.CurrentUserData.FirstOrDefault().currentUserName).ToList();
            return View(requests);
        }
        #endregion

        #region Feedback - Developer Contact
        public ActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback([Bind(Include = "requestSubjectTitle,requestCategoryName,requestReceiverID, requestDateTime, requestMessage, requestStatus, requestUsername")] Requests request)
        {
            if (ModelState.IsValid)
            {
                // Receiver Developer is Semih Elitas
                request.requestUsername = db.CurrentUserData.FirstOrDefault().currentUserName;
                request.requestStatus = false;
                request.requestDateTime = DateTime.Now;
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Home");
            }

            return View(request);
        }
        #endregion

        public ActionResult PatchNotes()
        {
            var notes = db.PatchNotes.ToList();
            return View(notes);
        }

        public ActionResult Documentation()
        {
            return View();
        }

        public ActionResult SystemMenu()
        {
            return View();
        }
    }
}