﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using CA2_v2;

namespace BootstrapMvcSample.Controllers
{
    
    public class HomeController : BootstrapBaseController
    {
        northwndEntities db = new northwndEntities();
        private static List<HomeInputModel> _models = ModelIntializer.CreateHomeInputModels();// 1
        
        public ActionResult Index()
        {
            //ViewBag.Title = "CA 2";
            ViewData["Title"] = "CA 2";
            //return View(); 
            //var homeInputModels = _models; //1                       
            //return View(homeInputModels); //1

            return View();

        }

        [HttpPost]
        public ActionResult Create(HomeInputModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _models.Count==0?1:_models.Select(x => x.Id).Max() + 1;
                _models.Add(model);
                Success("Your information was saved!");
                return RedirectToAction("Index");
            }
            Error("there were some errors in your form.");
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new HomeInputModel());
        }

        public ActionResult Delete(int id)
        {
            _models.Remove(_models.Get(id));
            Information("Your widget was deleted");
            if(_models.Count==0)
            {
                Attention("You have deleted all the models! Create a new one to continue the demo.");
            }
            return RedirectToAction("index");
        }
        public ActionResult Edit(int id)
        {
            var model = _models.Get(id);
            return View("Create", model);
        }
        [HttpPost]        
        public ActionResult Edit(HomeInputModel model,int id)
        {
            if(ModelState.IsValid)
            {
                _models.Remove(_models.Get(id));
                model.Id = id;
                _models.Add(model);
                Success("The model was updated!");
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

		public ActionResult Details(int id)
        {
            var model = _models.Get(id);
            return View(model);
        }


        internal ActionResult Admin()
        {
            // used for demonstrationg route filters
            throw new NotImplementedException();
        }
    }
}
