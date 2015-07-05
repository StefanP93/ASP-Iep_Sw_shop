using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IEPProjekatPS120511.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;

namespace IEPProjekatPS120511.Controllers
{
    public class SW_ProductController : Controller
    {
        private IEPProjekatPS120511_dbEntities3 db = new IEPProjekatPS120511_dbEntities3();

        // GET: SW_Product
        public ActionResult Index()
        {
            if (User.IsInRole("RegUser"))
                return RedirectToAction("Index_RegUser");
            else if(User.IsInRole("SuperAdmin") )
                return RedirectToAction("Index_Admin");
            else
                return RedirectToAction("Index_Guest");

        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Index_Admin(string sortOrder, string currentFilter, string searchString, 
            string currentFilterPcLow, string currentFilterPcHigh, string SearchPriceLow, string SearchPriceHigh, int? page)
        {
            int low = 0, high = Int32.MaxValue;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var product = from m in db.SW_Product select m;

            product = product.Where(s => s.Deleted == 0);

            if (searchString != null )
            {
                page = 1;
                
            }
            else
            {
                searchString = currentFilter;
            }

            if (SearchPriceLow != null)
            {
                page = 1;
                if (SearchPriceLow != "")
                    low = Convert.ToInt32(SearchPriceLow);
                else low = 0;
            }
            else
            {
                SearchPriceLow = currentFilterPcLow;
                if (SearchPriceLow != null)
                    low = Convert.ToInt32(SearchPriceLow);
            }

            if (SearchPriceHigh != null)
            {
                page = 1;
                if (SearchPriceHigh != "")
                    high = Convert.ToInt32(SearchPriceHigh);
                else high = Int32.MaxValue;
            }
            else
            {
                SearchPriceHigh = currentFilterPcHigh;
                if (SearchPriceHigh!=null)
                    high = Convert.ToInt32(SearchPriceHigh);
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilterPcHigh = SearchPriceHigh;
            ViewBag.CurrentFilterPcLow = SearchPriceLow;
           

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Name.Contains(searchString));
            }

            if (low > 0)
            {
                product = product.Where(s => s.Price>low);
            }

            if (high < Int32.MaxValue)
            {
                product = product.Where(s => s.Price < high);
            }


            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                default:
                    product = product.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

         [Authorize(Roles = "SuperAdmin, RegUser")]
        public ActionResult Index_RegUser(string sortOrder, string currentFilter, string searchString, 
            string currentFilterPcLow, string currentFilterPcHigh, string SearchPriceLow, string SearchPriceHigh, int? page)
        {
            int low = 0, high = Int32.MaxValue;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var product = from m in db.SW_Product select m;

            product = product.Where(s => s.Deleted == 0);

            if (searchString != null )
            {
                page = 1;
                
            }
            else
            {
                searchString = currentFilter;
            }

            if (SearchPriceLow != null)
            {
                page = 1;
                if (SearchPriceLow != "")
                    low = Convert.ToInt32(SearchPriceLow);
                else low = 0;
            }
            else
            {
                SearchPriceLow = currentFilterPcLow;
                if (SearchPriceLow != null)
                    low = Convert.ToInt32(SearchPriceLow);
            }

            if (SearchPriceHigh != null)
            {
                page = 1;
                if (SearchPriceHigh != "")
                    high = Convert.ToInt32(SearchPriceHigh);
                else high = Int32.MaxValue;
            }
            else
            {
                SearchPriceHigh = currentFilterPcHigh;
                if (SearchPriceHigh!=null)
                    high = Convert.ToInt32(SearchPriceHigh);
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilterPcHigh = SearchPriceHigh;
            ViewBag.CurrentFilterPcLow = SearchPriceLow;
           

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Name.Contains(searchString));
            }

            if (low > 0)
            {
                product = product.Where(s => s.Price>low);
            }

            if (high < Int32.MaxValue)
            {
                product = product.Where(s => s.Price < high);
            }


            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                default:
                    product = product.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index_Guest(string sortOrder, string currentFilter, string searchString,
            string currentFilterPcLow, string currentFilterPcHigh, string SearchPriceLow, string SearchPriceHigh, int? page)
        {
            int low = 0, high = Int32.MaxValue;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var product = from m in db.SW_Product select m;

            product = product.Where(s => s.Deleted == 0);

            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            if (SearchPriceLow != null)
            {
                page = 1;
                if (SearchPriceLow != "")
                    low = Convert.ToInt32(SearchPriceLow);
                else low = 0;
            }
            else
            {
                SearchPriceLow = currentFilterPcLow;
                if (SearchPriceLow != null)
                    low = Convert.ToInt32(SearchPriceLow);
            }

            if (SearchPriceHigh != null)
            {
                page = 1;
                if (SearchPriceHigh != "")
                    high = Convert.ToInt32(SearchPriceHigh);
                else high = Int32.MaxValue;
            }
            else
            {
                SearchPriceHigh = currentFilterPcHigh;
                if (SearchPriceHigh != null)
                    high = Convert.ToInt32(SearchPriceHigh);
            }

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilterPcHigh = SearchPriceHigh;
            ViewBag.CurrentFilterPcLow = SearchPriceLow;


            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.Name.Contains(searchString));
            }

            if (low > 0)
            {
                product = product.Where(s => s.Price > low);
            }

            if (high < Int32.MaxValue)
            {
                product = product.Where(s => s.Price < high);
            }


            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    product = product.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                default:
                    product = product.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(product.ToPagedList(pageNumber, pageSize));
        }

         [Authorize(Roles = "SuperAdmin")]
        // GET: SW_Product/Details/5
        public ActionResult Details_Admin(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_Product sW_Product = db.SW_Product.Find(id);
            if (sW_Product == null)
            {
                return HttpNotFound();
            }
            return View(sW_Product);
        }
         [Authorize(Roles = "SuperAdmin, RegUser")]
        public ActionResult Details_RegUser(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_Product sW_Product = db.SW_Product.Find(id);
            if (sW_Product == null)
            {
                return HttpNotFound();
            }
            return View(sW_Product);
        }

        public ActionResult Details_Guest(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_Product sW_Product = db.SW_Product.Find(id);
            if (sW_Product == null)
            {
                return HttpNotFound();
            }
            return View(sW_Product);
        }


        // GET: SW_Product/Create
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SW_Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDProduct,Name,Tag,Description,LogoToUpload,ImageToUpload,Price,Deleted")] SW_Product sW_Product)
        {
            if (ModelState.IsValid)
            {

                sW_Product.Image = new byte[sW_Product.ImageToUpload.ContentLength];
                sW_Product.ImageToUpload.InputStream.Read(sW_Product.Image, 0, sW_Product.Image.Length);

                sW_Product.Logo = new byte[sW_Product.LogoToUpload.ContentLength];
                sW_Product.LogoToUpload.InputStream.Read(sW_Product.Logo, 0, sW_Product.Logo.Length);

                sW_Product.Deleted = 0;

                db.SW_Product.Add(sW_Product);
                db.SaveChanges();
                return RedirectToAction("Index_Admin");
            }

            return View(sW_Product);
        }

        // GET: SW_Product/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_Product sW_Product = db.SW_Product.Find(id);
            if (sW_Product == null)
            {
                return HttpNotFound();
            }
            return View(sW_Product);
        }

        // POST: SW_Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDProduct,Name,Tag,Description,LogoToUpload,ImageToUpload,Price,Deleted")] SW_Product sW_Product)
        {
            if (ModelState.IsValid)
            {
                sW_Product.Image = new byte[sW_Product.ImageToUpload.ContentLength];
                sW_Product.ImageToUpload.InputStream.Read(sW_Product.Image, 0, sW_Product.Image.Length);

                sW_Product.Logo = new byte[sW_Product.LogoToUpload.ContentLength];
                sW_Product.LogoToUpload.InputStream.Read(sW_Product.Logo, 0, sW_Product.Logo.Length);

                db.Entry(sW_Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_Admin");
            }
            return View(sW_Product);
        }

        // GET: SW_Product/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SW_Product sW_Product = db.SW_Product.Find(id);
            if (sW_Product == null)
            {
                return HttpNotFound();
            }
            return View(sW_Product);
        }

        // POST: SW_Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SW_Product sW_Product = db.SW_Product.Find(id);
            sW_Product.Deleted = 1;
            db.Entry(sW_Product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index_Admin");
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
