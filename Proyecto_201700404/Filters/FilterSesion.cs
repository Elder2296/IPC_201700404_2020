using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Models;

namespace Proyecto_201700404.Filters
{
    public class FilterSesion:ActionFilterAttribute
    {
        private USUARIO usuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                usuario = (USUARIO)HttpContext.Current.Session["user"];
                if (usuario == null)
                {

                    if (filterContext.Controller is Controllers.HomeController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Home/Login");
                    }

                }
                else {


                    if (filterContext.Controller is Controllers.HomeController==true) {
                        filterContext.HttpContext.Response.Redirect("~/Admin/Admin");
                    }
                }


            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Home/Home");
                //HttpContext.Current.Session.Clear();
            }
            
        }
    }
}