using BubbleTea.DataAccess;
using BubbleTea.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BubbleTea.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IDataRepository _repository;
        public MenuController(IDataRepository respository)
        {
            _repository = respository;
        }
        // GET: Menu
        public ViewResult Index()
        {
            var baseTea = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Base Tea").ToList();
            var flavours = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Flavor").ToList();
            var toppinngs = _repository.GetAllGroupItems().Where(p => p.GroupOffering.Name == "Toppings").ToList();
            var sizes = _repository.GetAllSizes();
            MenuViewModel mv = new MenuViewModel(_repository) { BaseTea=baseTea, Flavors = flavours, Toppings = toppinngs, Sizes = sizes }; 
            return View(mv);
        }

        [HttpPost]
        public ViewResult Index(MenuViewModel model)
        {
            return View(model);
        }

        // GET: Menu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Menu/Create
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

        // GET: Menu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Menu/Edit/5
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

        // GET: Menu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Menu/Delete/5
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
