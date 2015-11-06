using Microsoft.AspNet.Identity;
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
            if (User.Identity.IsAuthenticated)
            {
                return View(GET.GeneratoreNotizie(User.Identity.GetUserId().ToString()));
            }
            else
            {
                return View(GET.GeneratoreNotizie(null));
            }
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            Provider p = new Provider();
            
            return View(p.LUrl);
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