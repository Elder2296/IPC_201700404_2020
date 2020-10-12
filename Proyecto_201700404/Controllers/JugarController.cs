using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Models;
using Proyecto_201700404.Models.ViewModels;
using Proyecto_201700404.Clases;

namespace Proyecto_201700404.Controllers
{
    public class JugarController : Controller
    {
        // GET: Jugar
        //public static List<Ficha> fichas = new List<Ficha>();
        //public static int turno = 0;
        public ActionResult Jugar()
        {
            return View();
        }
        public ActionResult ConfiguracionFichas() {
            return View();
        }
        [HttpPost]
        public ActionResult ConfiguracionFichas(Elegirfichas color)
        {

            if (ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("se para la partida");
                System.Diagnostics.Debug.WriteLine("Colores elejidos: "+color.idcolor1);
                System.Diagnostics.Debug.WriteLine("Colores elegidos 2: "+ color.idcolor2);

                //var seed = Environment.TickCount;
                
                var turno = new Random().Next(1,100);
                System.Diagnostics.Debug.WriteLine("El turno le corresponde al: "+turno);
                if ((turno % 2) == 0)
                {
                    turno = 1;
                }
                else {
                    turno = 2;
                }
                Variables.turno = turno;
                System.Diagnostics.Debug.WriteLine("El turno le corresponde al: " + turno);

                Variables.fichasbasicas();
                return RedirectToAction("PartidaMaquina");                   
                
            }
            else {
                System.Diagnostics.Debug.WriteLine("Algo esta mal en el model");
                return View();
            }
            
        }
        public ActionResult PartidaMaquina() {
            string jugador = "";
            if (Variables.turno == 1)  //mostrata de quien sera el turno en la vista

            {
                Proyecto_201700404.Models.USUARIO user = (Proyecto_201700404.Models.USUARIO)Session["user"];
                jugador = user.nombreUsuario;
            }
            else
            {
                jugador = "Invitado";
            }
            @TempData["jugador"] = jugador;
            
            return View();
        }
        [HttpPost]
        public ActionResult PartidaMaquina(string idcelda) {
            System.Diagnostics.Debug.WriteLine(idcelda);
            string jugador = "";
            if (Variables.turno == 1)
            {
                Proyecto_201700404.Models.USUARIO user = (Proyecto_201700404.Models.USUARIO)Session["user"];
                jugador = user.nombreUsuario;
            }
            else {
                jugador = "Invitado";
            }

            if (idcelda == "tablero")
            {

            }
            else { 
                   
            char sep = '-';
                if (idcelda == null)
                {
                }
                else
                {
                    string[] datos = idcelda.Split(sep);
                    int fil = int.Parse(datos[0]);
                    int colum = int.Parse(datos[1]);

                    //if (Contenedor_.VerificarExistencia(fil, colum))
                    //{
                    //}
                    //else
                    //{


                    

                    //    System.Diagnostics.Debug.WriteLine(fil);
                    //    System.Diagnostics.Debug.WriteLine(colum);

                    //    MovimientoViewModel movimiento = new MovimientoViewModel();
                    //    movimiento.fila = fil;
                    //    movimiento.col = colum;

                    //    if (Contenedor_.turno == 1)
                    //    {
                    //        movimiento.color = "blanco";

                    //        Contenedor_.turno = 2;


                    //    }
                    //    else if (Contenedor_.turno == 2)
                    //    {
                    //        movimiento.color = "negro";

                    //        Contenedor_.turno = 1;


                    //    }
                    //    Contenedor_.movimientos.Add(movimiento);
                    //}



                }

            }



            

            @TempData["jugador"] = jugador;
            return PartialView("Tablero"/*, Contenedor_.movimientos*/);
            //return Json(celda, JsonRequestBehavior.AllowGet);
        }


    }
}