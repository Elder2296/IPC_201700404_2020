using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Posibilidad
    {
        List<Ficha> fichasAfectadas;
        public Posibilidad() {
            fichasAfectadas = new List<Ficha>();
        }
        public int Fila { set; get; }
        public int Columna { set; get; }
        public Ficha origen { set; get; }
        public List<Ficha> getLista() {
            return fichasAfectadas;
        }
    }
}