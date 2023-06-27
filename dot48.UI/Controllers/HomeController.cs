using dot48.Application.Interfaces;
using Microsoft.Practices.ObjectBuilder2;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;

namespace dot48.UI.Controllers
{
    public class HomeController : Controller
    {       

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome Skill dot48";

            return View();
        }
    }
}