using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Models;
using Proyecto_201700404.Models.ViewModels;
using Proyecto_201700404.Clases;
using System.IO;
using System.Xml;

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
        public FileResult Descargar() {
            string Rutasitio = Server.MapPath("~/");
            string nombreArch = "Archivo"+Variables.N_archivo;
            string rutacompleta = Path.Combine(Rutasitio+"/FilesDes/"+nombreArch+".xml");

            XmlWriter xmlWriter = XmlWriter.Create(rutacompleta);
            xmlWriter.WriteStartDocument();
           
            xmlWriter.WriteStartElement("tablero");
            xmlWriter.WriteString("\r\n\t");

            foreach (var ficha in Variables.fichas)
            {
                xmlWriter.WriteStartElement("ficha");


                xmlWriter.WriteString("\r\n\t\t");
                xmlWriter.WriteStartElement("color");
                xmlWriter.WriteString(ficha.color);
                xmlWriter.WriteEndElement();


                xmlWriter.WriteString("\r\n\t\t");
                xmlWriter.WriteStartElement("columna");
                xmlWriter.WriteString(this.regresarLetra(ficha.columna));
                xmlWriter.WriteEndElement();


                xmlWriter.WriteString("\r\n\t\t");
                xmlWriter.WriteStartElement("fila");
                xmlWriter.WriteString(ficha.fila.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteString("\r\n\t");



                //xmlWriter.WriteStartElement("");
                //xmlWriter.WriteEndElement(); xmlWriter.WriteStartElement("");
                //xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();

            }


            xmlWriter.WriteStartElement("siguienteTiro");

            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("\r\n\t\t");
            if (Variables.turno == 1) {
                xmlWriter.WriteString("negro");
            } else if (Variables.turno==2) {
                xmlWriter.WriteString("blanco");
            }
            xmlWriter.WriteEndElement();


            xmlWriter.WriteEndElement();
            xmlWriter.WriteString("\r\n");
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();
            Variables.N_archivo = Variables.N_archivo + 1;


            return File(rutacompleta,"application/xml",nombreArch+".xml");
            //return View();  
        }

        private string regresarLetra(int columna)
        {
            String letra = "";
            switch (columna) {
                case 1:
                    letra = "A";
                    break;
                case 2:
                    letra = "B";
                    break;
                case 3:
                    letra = "C";
                    break;
                case 4:
                    letra = "C";
                    break;
                case 5:
                    letra = "E";
                    break;
                case 6:
                    letra = "F";
                    break;
                case 7:
                    letra = "G";
                    break;
                case 8:
                    letra = "H";
                    break;
                default:
                    break;
            }
            return letra;
           
        }

        public ActionResult Ganador() {
            Ganador ganador = new Ganador();

            int fichasblancas = 0;
            int fichasnegras = 0;
            foreach (var item in Variables.fichas)
            {

                System.Diagnostics.Debug.WriteLine("Ficha color: "+item.color+" fila: "+item.fila+" columna: "+item.columna);
                if (item.color == "blanco") {
                    fichasblancas = fichasblancas + 1;
                } else if (item.color=="negro") {
                    fichasnegras = fichasnegras + 1;
                
                }

            }

            
            System.Diagnostics.Debug.WriteLine("Fichas blancas: " +fichasblancas);
            System.Diagnostics.Debug.WriteLine("Fichas negras:  "+fichasnegras);
            int fb = 0;
            int fn = 0;
                
            foreach (var item in Variables.fichas)
            {
                if (item.color=="blanco") {
                    fb = fb + 1;
                }

            }
            foreach (var item in Variables.fichas)
            {
                if (item.color == "negro")
                {
                    fn = fn + 1;
                }

            }

            System.Diagnostics.Debug.WriteLine("OTRO RESULTADO Fichas blancas: " + fb);
            System.Diagnostics.Debug.WriteLine("OTRO RESULTADO Fichas negras:  " + fn);

            var gan = (Proyecto_201700404.Models.USUARIO)Session["user"];
            if (fichasblancas > fichasnegras)
            {
                

                //ganador.ganador = gan.nombreUsuario;
                if (Variables.viene == 1)
                {
                    ganador.ganador = "Invitado";
                }
                else
                {
                    ganador.ganador = "Maquina";
                }
                ganador.perdedor = gan.nombreUsuario;
                ganador.fichasGanador = fichasblancas ;
                ganador.fichasPerdedor = fichasnegras;

                
                ganador.movimientosGanador = Variables.mov_jugador2;
                ganador.movimeintoPerdedor = Variables.mov_jugador1;


            }
            else if (fichasnegras > fichasblancas)
            {

                ganador.ganador = gan.nombreUsuario;
                if (Variables.viene == 1)
                {
                    ganador.perdedor = "Invitado";
                }
                else {
                    ganador.perdedor = "Maquina";
                }
                
                ganador.fichasGanador = fichasnegras ;
                ganador.fichasPerdedor = fichasblancas;
                ganador.movimientosGanador = Variables.mov_jugador1;
                ganador.movimeintoPerdedor = Variables.mov_jugador2;
            }
            else if (fichasnegras==fichasblancas) {
                ganador.ganador = "Empate";
                ganador.perdedor = "Invitado";
                ganador.fichasGanador = fichasnegras;
                ganador.fichasPerdedor = fichasblancas;
                ganador.movimientosGanador = Variables.mov_jugador1;
                ganador.movimeintoPerdedor = Variables.mov_jugador2;
            }

            System.Diagnostics.Debug.WriteLine("ESTOS DATOS TRAE LA VARIABLE GANADOR fichas ganador: "+ganador.fichasGanador);
            System.Diagnostics.Debug.WriteLine("ESTOS DATOS TRAE LA VARIABLE GANADOR fichas perdedor: " + ganador.fichasPerdedor);




           Variables.ganador = ganador;

            Variables.fichas.Clear();
            Variables.VaciarTablero();



            iGamegtEntities1 bdatos = new iGamegtEntities1();

            Juego juego = new Juego();
            juego.idjugador = gan.id_usuario;
            juego.ganador = Variables.ganador.ganador;
            juego.perdedor = Variables.ganador.perdedor;
            juego.mov_ganador = Variables.ganador.movimientosGanador;
            juego.mov_perdedor = Variables.ganador.movimeintoPerdedor;
            juego.fichas_ganador = Variables.ganador.fichasGanador;
            juego.fichas_perdedor = Variables.ganador.fichasPerdedor;

            bdatos.Juego.Add(juego);
            bdatos.SaveChanges();



            return View();
        }

        public ActionResult RegresarMenu() {
            return RedirectToAction("Player","Player");
        }
        public ActionResult ConfiguracionFichas() {
            return View();
        }
        public ActionResult Regresar() {
            Variables.fichas.Clear();
            Variables.VaciarTablero();
            Variables.mov_jugador1 = 0;
            Variables.mov_jugador2 = 0;


            return RedirectToAction("ConfiguracionFichas");
        }
        [HttpPost]
        public ActionResult ConfiguracionFichas(Elegirfichas color)
        {

            if (ModelState.IsValid)
            {
                //System.Diagnostics.Debug.WriteLine("se para la partida");
                System.Diagnostics.Debug.WriteLine("Colores elejidos: " + color.idcolor1);
                //System.Diagnostics.Debug.WriteLine("Colores elegidos 2: "+ color.idcolor2);

                //var seed = Environment.TickCount;

                var turno = new Random().Next(1,100);
                //System.Diagnostics.Debug.WriteLine("El turno le corresponde al: "+turno);


                if (color.idcolor1 == 1)
                {
                    Variables.color1 = "negro";
                    Variables.color2 = "blanco";
                }
                else if (color.idcolor1 == 2)
                {
                    Variables.color1 = "blanco";
                    Variables.color2 = "negro";

                }


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

                return RedirectToAction("PartidaMaquina");                   
                
            }
            else {
                System.Diagnostics.Debug.WriteLine("Algo esta mal en el model");
                return View();
            }
            
        }
        public ActionResult PartidaMaquina() {
            
            
            return View();
        }
        [HttpPost]
        public ActionResult PartidaMaquina(string idcelda) {
            
            
            //System.Diagnostics.Debug.WriteLine(idcelda);

            

            this.procesoJuego(Variables.turno,idcelda);

            @TempData["jugador"] = this.turnoJugador(Variables.turno);

            this.Verificar();






            return PartialView("Tablero"/*, Contenedor_.movimientos*/);
            //return Json(celda, JsonRequestBehavior.AllowGet);
        }
        public void Verificar() {


            int termino = 0;

            if (Variables.turno == 1)
            {
                Control control = new Control();
                control.buscarXColor(1);

                if (control.listadePosibilidades().Count() == 0)
                {
                    Control control2 = new Control();
                    control2.buscarXColor(2);
                    if (control2.listadePosibilidades().Count() == 0)
                    {
                        termino = 1;
                    }

                }
            }
            else if (Variables.turno == 2)
            {

                Control control = new Control();
                control.buscarXColor(2);

                if (control.listadePosibilidades().Count() == 0)
                {
                    Control control2 = new Control();
                    control2.buscarXColor(1);
                    if (control2.listadePosibilidades().Count() == 0)
                    {
                        termino = 1;
                    }

                }

            }

            if (termino == 1)
            {
                Variables.Se_termino = 1;
            }

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
                        Variables.mov_jugador1 = Variables.mov_jugador1 + 1;
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
                        Variables.mov_jugador2 = Variables.mov_jugador2 + 1;
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

            if (turno == 1)
            {
                Control control3 = new Control();
                control3.buscarXColor(turno);
                if (control3.listadePosibilidades().Count() == 0)
                {
                    Variables.turno = 2;
                }

            }
            else if (turno == 2)
            {
                Control control4 = new Control();
                control4.buscarXColor(turno);
                if (control4.listadePosibilidades().Count() == 0)
                {
                    Variables.turno = 1;
                }
            }



        }











        public ActionResult ContraMaquina() {
            if (Variables.viene == 1)
            {
                string jugador = "";
                var turno = new Random().Next(1, 100);
                if ((turno % 2) == 0)
                {
                    turno = 1;

                }
                else
                {
                    turno = 2;
                }

                if (turno == 1)
                {
                    Proyecto_201700404.Models.USUARIO user = (Proyecto_201700404.Models.USUARIO)Session["user"];
                    jugador = user.nombreUsuario;
                }
                else
                {
                    jugador = "Maquina";
                }

                Variables.turno = turno;
                Variables.fichasbasicas();
                @TempData["jugador"] = jugador;

            }
            else {
                this.Verificar();
            }
            

            return View();
        }
        [HttpPost]
        public ActionResult ContraMaquina(string idcelda)
        {

            if (Variables.turno==1) {
                this.procesoJuego(Variables.turno,idcelda);
                @TempData["jugador"] = this.turnoJugador(Variables.turno);
            }

            this.Verificar();

            return PartialView("TableroMaquina");
        }
        public ActionResult MovMaquina() {

            if (Variables.turno==2) {
                Control control2 = new Control();
                //control2.mostraTabla();
                control2.buscarXColor(Variables.turno);
                int n_items = control2.listadePosibilidades().Count();
                if (n_items == 0)
                {
                    Variables.turno = 1;
                }
                else {
                    int aleatorio = new Random().Next(1,n_items);
                    int c = 1;
                    foreach (var item in control2.listadePosibilidades())
                    {
                        if (c==aleatorio) {
                            Ficha ficha = new Ficha();
                            ficha.color = "blanco";
                            ficha.fila = item.Fila;
                            ficha.columna = item.Columna;
                            Variables.mov_jugador2 = Variables.mov_jugador2 + 1;
                            Variables.valides = 1;
                            @TempData["valides"] = TempData["jugador"];
                            control2.buscarfichaParavoltear(ficha);
                            Variables.fichas.Add(ficha);
                            Variables.tablero[ficha.fila-1,ficha.columna-1].estado="ocupado";
                            Variables.turno = 1;
                            break;
                        }

                        c = c + 1;
                    }


                }
                
            }

            this.turnoJugador(Variables.turno);

            Variables.viene = 2;
            return RedirectToAction("ContraMaquina");
        }

        

    }
}