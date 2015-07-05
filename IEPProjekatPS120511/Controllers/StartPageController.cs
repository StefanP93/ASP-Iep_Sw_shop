using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IEPProjekatPS120511.Controllers
{
    public class StartPageController : Controller
    {
        // GET: StartPage
        public ActionResult Index()
        {
            return View();
        }

        // GET: StartPage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StartPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StartPage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StartPage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StartPage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StartPage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StartPage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
