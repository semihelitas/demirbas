using DTS.Models;
using DTS.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DTS.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        /// <summary>
        ///  Products Actions/Methods
        ///  written by Semih Elitas
        /// </summary>

        #region Private Properties

        /// <summary>
        /// Database Store property.
        /// </summary>
        private DatabaseContext db = new DatabaseContext();


        #endregion

        #region List Method
        // GET: Products
        [RequireHttps]
        public ActionResult List()
        {
            var products = db.Product.Include(x => x.Category).Include(y => y.Person);
            return View(products.ToList());
        }
        #endregion

        #region Add Methods
        // GET: Products/Add
        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel pavm = new ProductViewModel();
            pavm.categoriesList = db.Category.ToList();
            pavm.personList = db.Person.ToList();

            return View(pavm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "ProductSerialNumber,CategoryID,ProductBrand,PersonID,RegisterDateTime,ProductAmount,ProductWarrantyDate,ServiceContact,ProductFeatures,ProductImage")] Product product, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    product.ProductImage = file.ToString();
                    var allowedExtensions = new[] {
                    ".Jpg", ".png", ".jpg", "jpeg"
                    };

                    var fileName = Path.GetFileName(file.FileName);
                    var ext = Path.GetExtension(file.FileName);

                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + product.ProductSerialNumber + ext; //appending the name with id  
                                                                                        // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("~/Content/images/Products/"), myfile);
                        product.ProductImage = myfile;
                        file.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }

                    db.Product.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("list");

                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }

            return View(product);
        }
        #endregion

        #region Update Methods
        // GET: Products/Edit/5
        public ActionResult Update(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ProductViewModel pavm = new ProductViewModel();
            pavm.categoriesList = db.Category.ToList();
            pavm.personList = db.Person.ToList();
            pavm.product = product;

            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "ProductSerialNumber,CategoryID,ProductBrand,PersonID,RegisterDateTime,ProductAmount,LocationID,ProductWarrantyDate,ServiceContact,ProductFeatures")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list");
            }
            return View(product);
        }
        #endregion

        #region Delete Methods
        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
