using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Casilla
    {
        public Casilla() {
            estado = "desocupado";
        }
        public string estado  { set;get; }
        public string valor { set; get; }

    }
}