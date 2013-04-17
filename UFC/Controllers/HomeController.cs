using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UFC.Controllers
{    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string login = Request.Params["login"];
            TempData["login"] = login;
            return RedirectToAction("SelecionaEvento","APOSTA");
            //ViewBag.Message = "Welcome to ASP.NET MVC!";
            //return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ComoFunciona()
        {
            return View();
        }

        public ActionResult Regras()
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }

        public ActionResult AcessoNegado()
        {
            return View();
        }
    }
}
