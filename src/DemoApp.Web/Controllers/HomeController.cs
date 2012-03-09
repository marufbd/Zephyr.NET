using System;
using System.Web.Mvc;
using DemoApp.Web.DomainModels;
using NHibernate;
using Zephyr.Web.Mvc.Controllers;

namespace DemoApp.Web.Controllers
{
    public class HomeController : ZephyrController
    { 
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to demo site!";
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
