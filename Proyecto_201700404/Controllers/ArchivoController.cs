using Proyecto_201700404.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Proyecto_201700404.Controllers
{
    public class ArchivoController : Controller
    {
        // GET: Archivo
        public ActionResult Archivo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Guardar(ArchivoViewModel archivo) {
            string Rutasitio = Server.MapPath("~/");
            string patharchivo = Path.Combine(Rutasitio+"/Files/"+archivo.CargarArchivo.FileName+".xml");
            if(!ModelState.IsValid){
                return View("Archivo", archivo);
            }
            archivo.CargarArchivo.SaveAs(patharchivo);//Revisar si sobreescribe


            XDocument docxml = XDocument.Load(patharchivo);

            var consulta = from datos in docxml.Descendants("ficha")
                           select new MovimientoViewModel
                           {
                               color=datos.Element("color").Value,
                               col=Columna(datos.Element("columna").Value),
                               fila=int.Parse(datos.Element("fila").Value)
                           };

            List<MovimientoViewModel> fichas = consulta.ToList();
            

            @TempData["Message"] = "Se cargo el archivo";

            @TempData["lista"] = fichas;
            //ViewBag.listafichas = fichas;

            //ViewBag
            //ViewData
            //Session[]
            //@TempData[]


            //return RedirectToAction("Partida","Player");

            return RedirectToAction("Partida","Player");
        }

        private int Columna(string letra) {
            int col = 0;
            switch (letra)
            {
                case "A":
                    col = 1;
                    break;
                case "B":
                    col = 2;
                    break;
                case "C":
                    col = 3;
                    break;
                case "D":
                    col = 4;
                    break;
                case "E":
                    col = 5;
                    break;
                case "F":
                    col = 6;
                    break;
                case "G":
                    col = 7;
                    break;
                case "H":
                    col = 8;
                    break;
                default:
                    col = 1;
                    break;



            }


            return col;
        
        }
    }
}