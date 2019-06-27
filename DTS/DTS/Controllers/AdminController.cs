using DTS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DTS.Controllers
{
    [Authorize(Roles = "1,3")] // Admin&Developer
    public class AdminController : Controller
    {
        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext db = new DatabaseContext();


        #endregion

        #region New User
        // GET: Admin
        public ActionResult NewUser()
        {
            ViewBag.CountUser = db.Login.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser([Bind(Include = "username, password, role_id")] Login newUser)
        {
            if (ModelState.IsValid)
            {
                List<Login> userList = db.Login.ToList();
                bool usernameEmpty = true;
                foreach (var user in userList)
                {
                    if(user.username == newUser.username)
                    {
                        usernameEmpty = false;
                        break;
                    }
                    else
                    {
                        usernameEmpty = true;
                    }
                }

                if(usernameEmpty == true)
                {
                    try
                    {
                        db.Login.Add(newUser);
                        db.SaveChanges();
                        return RedirectToAction("UserList");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Hata oluştu! Lütfen tekrar deneyiniz.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı kullanımda.");
                }
                
            }
            return View(newUser);
        }
        #endregion

        #region User List
        public ActionResult UserList()
        {
            ViewBag.CountUser = db.Login.Count();
            List<Login> users = db.Login.ToList();
            return View(users);
        }
        #endregion

        #region Update User
        // GET: Products/Edit/5
        public ActionResult UpdateUser(int id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login user = db.Login.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser([Bind(Include = "id,username,password,role_id")] Login user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                string currentUserName = db.CurrentUserData.Where(x => x.currentUserName == user.username).Select(x=>x.currentUserName).ToString();

                if (currentUserName == null)
                {
                    try
                    {
                        // Setting.
                        var ctx = Request.GetOwinContext();
                        var authenticationManager = ctx.Authentication;

                        // Sign Out.
                        authenticationManager.SignOut();

                        // Info.
                        return this.RedirectToAction("Login", "Account");
                    }
                    catch (Exception ex)
                    {
                        // Info
                        throw ex;
                    }
                }
                else
                {
                    return RedirectToAction("UserList");
                }
            }
            return View(user);
        }
        #endregion
    }
}