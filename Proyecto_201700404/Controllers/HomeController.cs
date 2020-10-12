using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Models;
using Proyecto_201700404.Models.ViewModels;

namespace Proyecto_201700404.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Registrar() {
            iGamegtEntities1 based = new iGamegtEntities1();

            
            var listaciudades = based.CIUDAD.ToList();
            var modelUsuarios = new UsuarioViewModel { ciudades = listaciudades };


            ViewBag.verificacion = 3;

            return View(modelUsuarios);
        
        }
     
        [HttpPost]
        public ActionResult Registrar(UsuarioViewModel model)
        {
            


            
            try
            {
                iGamegtEntities1 bdatos = new iGamegtEntities1();
                
                var listaciudades = bdatos.CIUDAD.ToList();
                var modelUsuarios = new UsuarioViewModel { ciudades = listaciudades };
                model.ciudades = modelUsuarios.ciudades;
                model.tipoUsuario="normal";
                var query = bdatos.USUARIO.Where(p => p.nombreUsuario == model.nombreUsuario).Select(p=>p);
                if (query.Count()>0)
                {
                    ViewBag.verificacion = 1;//ya existe un usuario igual en el sistema
                    return View(model);

                }
                else {

                    
                    if (!ModelState.IsValid)
                    {//si el modelo que estamos recibiendo no contiene ningun error 
                     //BDatos bdatos = new BDatos();
                        USUARIO user = new USUARIO();
                        user.nombre = model.nombre;
                        user.apellido = model.apellido;
                        user.nombreUsuario = model.nombreUsuario;
                        user.tipoUsuario = model.tipoUsuario;
                        user.contrasenia = model.contrasenia;
                        user.fechaNac = model.fechaNac;
                        user.email = model.email;
                        user.id_ciudad = model.id_ciudad;

                        bdatos.USUARIO.Add(user);
                        bdatos.SaveChanges();//Aqui se termina de guardar
                        return Redirect("~/Home/Login");
                    }
                    ViewBag.verificacion = 0;//no se puedo agregar
                    return View(model);
                }




                
                

                

            }
            catch (Exception)
            {

                throw;
            }
            

            

        }
        public ActionResult Login() {
            ViewBag.verificacion = 0;
            return View();
        }
        [HttpPost]
        public ActionResult Login(UsuarioViewModel model)
        {
            iGamegtEntities1 context = new iGamegtEntities1();
            

            try
            {
                var user = (from d in context.USUARIO
                             where d.nombreUsuario == model.nombreUsuario && d.contrasenia == model.contrasenia
                             select d).FirstOrDefault();//devuelve null o el objeto
                if (user == null)
                {
                    ViewBag.Error = "Credenciales incorrectas";
                    ViewBag.verificacion = 1;
                    return View(model);
                }
                else {
                    if (user.tipoUsuario == "normal")
                    {
                        Session["user"] = user;
                        return RedirectToAction("Player", "Player");//Action, controler

                    }
                    else if(user.tipoUsuario=="admin") {
                        Session["user"] = user;
                        return RedirectToAction("Admin", "Admin");//Action, controler


                    }

                    

                }
                return RedirectToAction("Home", "Home");
                
            }
            catch (Exception)
            {
                throw;
            }



           

            
        }
    }
}