using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Models.ViewModels;
using Proyecto_201700404.Models;
using Proyecto_201700404.Clases;

namespace Proyecto_201700404.Controllers
{
    public class XtremeController : Controller
    {
        // GET: Xtreme
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegistroInvitado() {
            return View();
        }
        [HttpPost]
        public ActionResult RegistroInvitado(InvitadoViewModel invitado) {
            
            if (ModelState.IsValid)
            {
                //System.Diagnostics.Debug.WriteLine(invitado.nombre);
                Variables.partida.IniciarPartida();
                
                Proyecto_201700404.Models.USUARIO user = (Proyecto_201700404.Models.USUARIO)Session["user"];
               
                Jugador contrincante = new Jugador();//el usuario invitado
             
                //Se agregan a los dos jugadores a la partida
                Variables.partida.JugadorLocal().Alias=user.nombreUsuario;
                Variables.partida.JugadorInvitado().Alias = invitado.nombre;


                return RedirectToAction("RegistroFichas");
            }
            else {
                return View();
            }
            
        }
        public ActionResult RegistroFichas() {
            return View();
        }
        [HttpPost]
        public ActionResult FichasHost(ColorFichaViewModel colorficha) {
            if (ModelState.IsValid)
            {
                iGamegtEntities1 db = new iGamegtEntities1();

                var col = (from d in db.ColorFicha where d.idcolor == colorficha.idcolor select d).FirstOrDefault();

                Ficha ficha = new Ficha();
                ficha.color = col.color;
                ficha.clase = col.clase;
                int repetido = 0;
                if (Variables.partida.JugadorLocal().puedoAgregarfichas() == true && Variables.partida.JugadorLocal().fichaRepetida(ficha) != true)
                {
                    //si su lista es menor a 5 y que el color no sea repetido
                    foreach (var fichacontrincante in Variables.partida.JugadorInvitado().MisFichas())
                    {
                        if (fichacontrincante.color == ficha.color)
                        {
                            repetido = 1;
                        }

                    }
                    if (repetido == 0)
                    {
                        ficha.Turnodueño = 1;
                        Variables.partida.JugadorLocal().MisFichas().Add(ficha);
                    }



                }
                return RedirectToAction("RegistroFichas");

            }
            else {
                return RedirectToAction("RegistroFichas");
            }
            
            //System.Diagnostics.Debug.WriteLine("LAS FICHAS DEL JUGADOR LOCAL");

            //foreach (var item in Variables.partida.JugadorLocal().MisFichas())
            //{
            //    System.Diagnostics.Debug.WriteLine("color: "+item.color+"  clase: "+item.clase);

            //}

            //System.Diagnostics.Debug.WriteLine("COLORES DE HOST  "+col.color+" clase: "+col.clase);
           
        }
        [HttpPost]
        public ActionResult FichasInvitado(ColorFichaViewModel colorficha)
        {

            if (ModelState.IsValid)
            {
                iGamegtEntities1 db = new iGamegtEntities1();

                var col = (from d in db.ColorFicha where d.idcolor == colorficha.idcolor select d).FirstOrDefault();

                Ficha ficha = new Ficha();
                ficha.color = col.color;
                ficha.clase = col.clase;
                int repetido = 0;
                if (Variables.partida.JugadorInvitado().puedoAgregarfichas() == true && Variables.partida.JugadorInvitado().fichaRepetida(ficha) != true)
                {
                    foreach (var fichacontrincante in Variables.partida.JugadorLocal().MisFichas())
                    {
                        if (fichacontrincante.color == ficha.color)
                        {
                            repetido = 1;
                        }

                    }
                    if (repetido == 0)
                    {
                        ficha.Turnodueño = 2;

                        Variables.partida.JugadorInvitado().MisFichas().Add(ficha);
                    }
                }
                return RedirectToAction("RegistroFichas");

            }
            else {
                return RedirectToAction("RegistroFichas");
            }   
           
        }


        public ActionResult Requisitos() {
            if (Variables.partida.JugadorInvitado().MisFichas().Count() != 0 && Variables.partida.JugadorLocal().MisFichas().Count() != 0)
            {
                return RedirectToAction("ConfiguracionTablero");
            }
            else {
                return RedirectToAction("RegistroFichas");
            }
        }




        public ActionResult ConfiguracionTablero() {
            return View();
        }




        [HttpPost]
        public ActionResult ConfiguracionTablero(TableroViewModel tablero) {
            if (ModelState.IsValid)
            {
                Variables.partida.IniciarTablero(tablero.Fila,tablero.Columna);
                return RedirectToAction("Jugar","JugarXtreme");
            }
            else {
                return View();
            }
            
        }





       
       
        public ActionResult Xtreme() {
            return View();
        }
        
       
    }
}