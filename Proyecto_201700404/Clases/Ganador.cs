using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Ganador
    {
        public string ganador { set; get; }
        public string perdedor { set; get; }

        public int movimientosGanador { set; get; }
        public int movimeintoPerdedor { set; get; }
        public int fichasGanador { set; get; }
        public int fichasPerdedor { set; get; }
    }
}