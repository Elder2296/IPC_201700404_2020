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
            
            
            //System.Diagnostics.Debug.WriteLine(idcelda);

            

            this.procesoJuego(Variables.turno,idcelda);

            @TempData["jugador"] = this.turnoJugador(Variables.turno);








            return PartialView("Tablero"/*, Contenedor_.movimientos*/);
            //return Json(celda, JsonRequestBehavior.AllowGet);
        }

        public string turnoJugador(int turno) {
            string nombre = "";

            if (Variables.turno == 1)
            {
                Proyecto_201700404.Models.USUARIO user = (Proyecto_201700404.Models.USUARIO)Session["user"];
                nombre = user.nombreUsuario;
            }
            else
            {
                nombre = "Invitado";
            }

            return nombre;
        }

        public void procesoJuego(int turno,string idcelda) {
            
            

            if (idcelda == "tablero" || idcelda == null)
            {

            }
            else
            {
                
                char sep = '-';

                string[] datos = idcelda.Split(sep);
                int fil = int.Parse(datos[0]);
                int colum = int.Parse(datos[1]);

                int encontro = 0;
                System.Diagnostics.Debug.WriteLine("AL TURNO QUE LE TOCA AHORITA ES: " + Variables.turno);

                


                Ficha ficha = new Ficha();
                ficha.fila = fil;
                ficha.columna = colum;

                if (Variables.turno == 1)
                {
                    Control control = new Control();
                    //control.mostraTabla();
                    control.buscarXColor(Variables.turno);
                    ficha.color = "negro";
                    foreach (var item in control.listadePosibilidades())
                    {

                        if (item.Columna==ficha.columna && item.Fila==ficha.fila) {
                            encontro = 1;
                        }
                        //System.Diagnostics.Debug.WriteLine("hay posibilidades en fila: " + item.Fila + "  columna:  " + item.Columna);
                        //foreach (var item2 in item.getLista())
                        //{
                        //    System.Diagnostics.Debug.WriteLine("Fichas que se voltearan   color: " + item2.color + "  fila: " + item2.fila + "  columna: " + item2.columna);

                        //}


                    }
                    if (encontro == 1)
                    {//la posicion ingresada si es valida 

                        Variables.valides = 1;
                        @TempData["valides"] = TempData["jugador"];
                        control.buscarfichaParavoltear(ficha);//voltea las fichas
                        Variables.fichas.Add(ficha);//agrega a mi lista de fichas
                        Variables.tablero[ficha.fila - 1, ficha.columna - 1].estado = "ocupado";//cambio el estado de la casilla ocupada

                    }
                    else {
                        Variables.valides = 2;
                        @TempData["valides"] = TempData["jugador"];
                        //la jugada es invalida
                    }
                    
                    
                    
                    
                    Variables.turno = 2;
                }
                else if (Variables.turno == 2)
                {
                    Control control2 = new Control();
                    control2.mostraTabla();
                    control2.buscarXColor(Variables.turno);
                    ficha.color = "blanco";
                    foreach (var item in control2.listadePosibilidades())
                    {
                        if (item.Columna == ficha.columna && item.Fila == ficha.fila)
                        {
                            encontro = 1;
                        }

                        //System.Diagnostics.Debug.WriteLine("hay posibilidades en fila: " + item.Fila + "  columna:  " + item.Columna);
                        //foreach (var item2 in item.getLista())
                        //{
                        //    System.Diagnostics.Debug.WriteLine("Fichas que se voltearan   color: " + item2.color + "  fila: " + item2.fila + "  columna: " + item2.columna);

                        //}


                    }

                    if (encontro == 1)
                    {
                        Variables.valides = 1;
                        @TempData["valides"] = TempData["jugador"];
                        control2.buscarfichaParavoltear(ficha);
                        Variables.fichas.Add(ficha);
                        Variables.tablero[ficha.fila - 1, ficha.columna - 1].estado = "ocupado";
                    }
                    else {
                        Variables.valides = 2;
                        @TempData["valides"] = TempData["jugador"];

                        //la jugada es invalida
                    }



                    
                    
                    
                    Variables.turno = 1;

                }

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


    }
}