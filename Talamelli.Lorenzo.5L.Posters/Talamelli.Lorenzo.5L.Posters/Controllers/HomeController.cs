using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Talamelli.Lorenzo._5L.Posters.Models;

namespace Talamelli.Lorenzo._5L.Posters.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Post()
        {
            Notizia GET = new Notizia();
            return View(GET.GeneratoreNotizie());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}