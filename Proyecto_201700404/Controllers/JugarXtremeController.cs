using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Clases;
using Proyecto_201700404.Models;

namespace Proyecto_201700404.Controllers
{
    public class JugarXtremeController : Controller
    {
        // GET: JugarXtreme
        public ActionResult Jugar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Jugar(string idcelda) {
            if (idcelda == "tablero" || idcelda == null)
            {
                //System.Diagnostics.Debug.WriteLine("SIGUE PASANDO ACA ESTA COCHINADA");
            }
            else {
                char sep = '-';

                string[] datos = idcelda.Split(sep);
                int fil = int.Parse(datos[0]);
                int colum = int.Parse(datos[1]);

                //System.Diagnostics.Debug.WriteLine("FILA: "+fil+"  COLUMNA:  "+colum);
                Variables.partida.PonerFicha(fil,colum);   
            }

            Validar validar = new Validar(Variables.partida.getListaFichas(),Variables.partida.getFilasTablero(),Variables.partida.getColumnasTabler());
            validar.Buscar(1);
            Validar validacion = new Validar(Variables.partida.getListaFichas(), Variables.partida.getFilasTablero(), Variables.partida.getColumnasTabler());
            validacion.Buscar(2);

            if (validar.listadePosibilidades().Count()==0 && validacion.listadePosibilidades().Count()==0) {
                Variables.Se_termino = 1;
            }


            return PartialView("Tablero");
            
        }

        public ActionResult verGanador() {
            Ganador ganador = new Ganador();

            int fichaslocal = 0;
            int fichasinvitado = 0;
            foreach (var item in Variables.partida.getListaFichas())
            {
                if (item.Turnodueño == 1)
                {
                    fichaslocal = fichaslocal + 1;
                }
                else {
                    fichasinvitado = fichasinvitado + 1;
                }

            }

            if (fichaslocal > fichasinvitado)
            {
                ganador.ganador = Variables.partida.JugadorLocal().Alias;
                ganador.fichasGanador = fichaslocal;
                ganador.perdedor = Variables.partida.JugadorInvitado().Alias;
                ganador.fichasPerdedor = fichasinvitado;
                ganador.movimientosGanador = Variables.partida.JugadorLocal().getMovimientos();
                ganador.movimeintoPerdedor = Variables.partida.JugadorInvitado().getMovimientos();
            }
            else if (fichasinvitado > fichaslocal)
            {
                ganador.ganador = Variables.partida.JugadorInvitado().Alias;
                ganador.fichasGanador = fichasinvitado;
                ganador.perdedor = Variables.partida.JugadorLocal().Alias;
                ganador.fichasPerdedor = fichaslocal;
                ganador.movimientosGanador = Variables.partida.JugadorInvitado().getMovimientos();
                ganador.movimeintoPerdedor = Variables.partida.JugadorLocal().getMovimientos();

            }
            else {
                ganador.ganador = "EMPATE";
                ganador.fichasGanador = fichasinvitado;
                ganador.perdedor = Variables.partida.JugadorLocal().Alias;
                ganador.fichasPerdedor = fichaslocal;
                ganador.movimientosGanador = Variables.partida.JugadorLocal().getMovimientos();
                ganador.movimeintoPerdedor = Variables.partida.JugadorInvitado().getMovimientos();

            }

            var gan = (Proyecto_201700404.Models.USUARIO)Session["user"];

            Variables.ganador = ganador;


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
            Variables.partida.IniciarPartida();

            return View();
        }


    }
}