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
    
    public class PlayerController : Controller
    {
        // GET: Player

        
        public ActionResult Player()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"].ToString();

            return View();
        }
        public ActionResult Partida() {
            if (TempData["lista"] == null)
            {
                return View();
            }
            else {
                ViewBag.lista = TempData["lista"];
                return View();
            }


        }
        public ActionResult Estadisticas() {
            var jugador = (Proyecto_201700404.Models.USUARIO)Session["user"];
            iGamegtEntities1 bdatos = new iGamegtEntities1();

            Variables.juegos  = (from d in bdatos.Juego where d.idjugador == jugador.id_usuario select d).ToList();

           
            return View();
        }




        [HttpPost]
        public ActionResult Posicion(string idcelda) {
            idcelda += " es la celdad seleccionada. ";
            return Json(idcelda, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Movimiento(string idcelda) {
            //@TempData["lista"] = null;
            //System.Diagnostics.Debug.WriteLine("entro a acticon movimiento");
            //return PartialView("_tablero");
            idcelda += " es la celdad seleccionada. ";
            return Json(idcelda, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult Partida(string idcelda) {
            //iGamegtEntities1 based = new iGamegtEntities1();

            //Movimiento mov = new Movimiento();
            //char separador = '-';
            //string[] valores = idcelda.Split(separador);
            //int fila = int.Parse(valores[0]);
            //int columna = int.Parse(valores[1]);
            //mov.col = columna;
            //mov.fila = fila;
            //mov.color = "negro";

            //based.Movimiento.Add(mov);
            //based.SaveChanges();



            ////List<Ficha> fichas = new List<Ficha>();
            ////Ficha ficha;
            
            
            ////if (turno == 1)
            ////{
            ////    ficha = new Ficha();
            ////    ficha.color = "blanco";
            ////    turno = 0;


            ////}
            ////else {
            ////    ficha = new Ficha();
            ////    ficha.color = "negro";
            ////    turno = 1;

            ////}
            ////ficha.columna = columna;
            ////ficha.fila = fila;

            //////fichas.Add(ficha);
            ////Ficha ficha1 = new Ficha();
            ////Ficha ficha2 = new Ficha();
            ////Ficha ficha3 = new Ficha();
            ////ficha1.color = "blanco";
            ////ficha1.columna = 3;
            ////ficha1.fila = 3;
            ////ficha2.color = "negro";
            ////ficha2.columna = 4;
            ////ficha2.fila = 4;
            ////ficha3.color = "blanco";
            ////ficha3.columna = 3;
            ////ficha3.fila = 3;
            ////fichas.Add(ficha1);
            ////fichas.Add(ficha2);
            ////fichas.Add(ficha3);




            //ViewBag.lista = based.Movimiento.ToList();


            //return RedirectToAction("Partida","Player"); 
            return PartialView("Partida");
        
        }
        public ActionResult Tablero() {

            return View();
        }
        [HttpPost]
        public ActionResult Tablero(string idcelda)
        {
            //using (iGamegtEntities1 based = new iGamegtEntities1()) {
            //    Movimiento mov = new Movimiento();
            //    char separador = '-';
            //    string[] valores = idcelda.Split(separador);
            //    int fila = int.Parse(valores[0]);
            //    int columna = int.Parse(valores[1]);
            //    mov.col = columna;
            //    mov.fila = fila;
            //    mov.color = "negro";



            //    ViewBag.lista = based.Movimiento.ToList();

            //    return View();


            //} ;

            idcelda += " es la celdad seleccionada. ";
            return PartialView("_tablero");



        }
    }
}