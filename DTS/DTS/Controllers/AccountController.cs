using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using DTS;
using DTS.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DTS.Controllers
{
    /// <summary>  
    /// Account controller class.    
    /// </summary>  
    public class AccountController : Controller
    {
        /// <summary>
        ///  Account Actions
        ///  written by Semih Elitas
        /// </summary>

        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext databaseManager = new DatabaseContext();


        #endregion

        #region Default Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        public AccountController()
        {
        }

        #endregion

        #region Login methods

        /// <summary>
        /// GET: /Account/Login
        /// </summary>
        /// <param name="returnUrl">Return URL parameter</param>
        /// <returns>Return login view</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                // Verification.
                if (this.Request.IsAuthenticated)
                {
                    // Info.
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Info.
            return this.View();
        }

        /// <summary>
        /// POST: /Account/Login
        /// </summary>
        /// <param name="model">Model parameter</param>
        /// <param name="returnUrl">Return URL parameter</param>
        /// <returns>Return login view</returns>
        /// 

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl, string userName)
        {
            try
            {
                // Verification.
                if (ModelState.IsValid)
                {
                    // Initialization.
                    var loginInfo = this.databaseManager.LoginByUsernamePassword(model.Username, model.Password).ToList();

                    // Verification.
                    if (loginInfo != null && loginInfo.Count() > 0)
                    {
                        // Initialization.
                        var logindetails = loginInfo.First();

                        // Login In.
                        this.SignInUser(logindetails.username, logindetails.role_id, false);

                        // setting.
                        this.Session["role_id"] = logindetails.role_id;

                        // Mevcut kullanıcı bilgilerini girer
                        var currentUserInfo = this.databaseManager.CurrentUserData.ToList();
                        if(currentUserInfo.Count() == 0)
                        {
                            CurrentUserData currentUser = new CurrentUserData();
                            currentUser.currentUserRoleID = logindetails.role_id;
                            currentUser.currentUserName = logindetails.username;

                            this.databaseManager.CurrentUserData.Add(currentUser);
                            databaseManager.SaveChanges();
                        }

                        // Info.
                        return this.RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        // Setting.
                        ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı/Şifre Eşleşmesi");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        #endregion

        #region Log Out method.

        /// <summary>
        /// POST: /Account/LogOff
        /// </summary>
        /// <returns>Return log off action</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                // Setting.
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;

                // Sign Out.
                authenticationManager.SignOut();
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Mevcut kullanıcı bilgilerini siler
            var currentUserInfo = this.databaseManager.CurrentUserData.ToList();
            var currentUser = this.databaseManager.CurrentUserData.FirstOrDefault();
            databaseManager.CurrentUserData.Remove(currentUser);
            databaseManager.SaveChanges();

            // Info.
            return this.RedirectToAction("Login", "Account");
        }

        #endregion

        #region Sign In method.

        /// <summary>
        /// Sign In User method.
        /// </summary>
        /// <param name="username">Username parameter.</param>
        /// <param name="role_id">Role ID parameter</param>
        /// <param name="isPersistent">Is persistent parameter.</param>
        private void SignInUser(string username, int role_id, bool isPersistent)
        {
            // Initialization.
            var claims = new List<Claim>();

            try
            {
                // Setting
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.Role, role_id.ToString()));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;

                // Sign In.
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }
        }

        #endregion

        #region Redirect to local method.

        /// <summary>
        /// Redirect to local method.
        /// </summary>
        /// <param name="returnUrl">Return URL parameter.</param>
        /// <returns>Return redirection action</returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info
                throw ex;
            }

            // Info.
            return this.RedirectToAction("List", "Products");
        }

        #endregion

        #region Roles method
        [Authorize]
        public RedirectToRouteResult GetSettings()
        {
            var currentUser = databaseManager.CurrentUserData.ToList();
            var currentUserRole = currentUser.FirstOrDefault().currentUserRoleID;

            if (currentUserRole == 1)
            {
                return RedirectToAction("AdminSettings");
            }
            else if (currentUserRole == 2)
            {
                return RedirectToAction("Settings");
            }
            else if (currentUserRole == 3)
            {
                return RedirectToAction("DeveloperSettings");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [Authorize(Roles = "1")]
        public ActionResult AdminSettings()
        {
            ViewBag.CountUser = databaseManager.Login.Count();
            return View();
        }

        [Authorize(Roles = "2")]
        public ActionResult Settings()
        {
            //var currentUser = databaseManager.CurrentUserData.FirstOrDefault().currentUserName;
            //Login login = databaseManager.Login.Where(x=>x.username == currentUser).FirstOrDefault();

            //if (login == null)
            //{
            //    return HttpNotFound();
            //}

            return View();
        }

        [Authorize(Roles = "3")]
        public ActionResult DeveloperSettings()
        {
            return View();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminSettings(Login login, string currentPassword, string newPassword, string newPasswordAgain)
        {
            if (ModelState.IsValid)
            {
                var currentUser = databaseManager.CurrentUserData.FirstOrDefault().currentUserName;
                login = databaseManager.Login.Where(x => x.username == currentUser).FirstOrDefault();

                if (currentPassword != "" && newPassword != "" && newPasswordAgain != "")
                {
                    if (currentPassword == login.password)
                    {
                        if (newPassword == newPasswordAgain)
                        {
                            login.password = newPassword;
                            databaseManager.SaveChanges();
                            return PartialView("~/Views/Account/SuccessModal.cshtml");
                            //ModelState.AddModelError(string.Empty, "Değişiklikler başarıyla kaydedildi!");
                            //return Redirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Yeni parola verileri uyuşmamaktadır.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "İşlem başarısız, lütfen tekrar deneyiniz!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lütfen eksik veri girişi yapmayınız!");
                }
            }
            return View(login);
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Settings(Login login, string currentPassword, string newPassword, string newPasswordAgain)
        {
            if (ModelState.IsValid)
            {
                var currentUser = databaseManager.CurrentUserData.FirstOrDefault().currentUserName;
                login = databaseManager.Login.Where(x=>x.username == currentUser).FirstOrDefault();

                if(currentPassword != "" && newPassword != "" && newPasswordAgain != "")
                {
                    if (currentPassword == login.password)
                    {
                            if (newPassword == newPasswordAgain)
                            {
                                login.password = newPassword;
                                databaseManager.SaveChanges();
                                return PartialView("~/Views/Account/SuccessModal.cshtml");
                            //ModelState.AddModelError(string.Empty, "Değişiklikler başarıyla kaydedildi!");
                            //return Redirect("/");
                        }
                            else
                            {
                                ModelState.AddModelError(string.Empty, "Yeni parola verileri uyuşmamaktadır.");
                            }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "İşlem başarısız, lütfen tekrar deneyiniz!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lütfen eksik veri girişi yapmayınız!");
                } 
            }
            return View(login);
        }
        #endregion   
    }
}