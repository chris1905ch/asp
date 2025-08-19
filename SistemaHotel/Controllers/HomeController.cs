using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaHotel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Redirigir al login si no hay sesión activa
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            // Redirigir al menú principal
            return RedirectToAction("Menu");
        }

        public ActionResult Menu()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult About()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}