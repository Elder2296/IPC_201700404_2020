using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_201700404.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult Cerrar() {
            Session["user"] = null;
            return RedirectToAction("Home","Home");
        }
    }
}