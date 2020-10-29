using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_201700404.Clases;
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
                System.Diagnostics.Debug.WriteLine("SIGUE PASANDO ACA ESTA COCHINADA");
            }
            else {
                char sep = '-';

                string[] datos = idcelda.Split(sep);
                int fil = int.Parse(datos[0]);
                int colum = int.Parse(datos[1]);

                System.Diagnostics.Debug.WriteLine("FILA: "+fil+"  COLUMNA:  "+colum);
                Variables.partida.PonerFicha(fil,colum);   
            }

            return PartialView("Tablero");
            
        }
    }
}