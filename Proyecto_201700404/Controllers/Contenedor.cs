using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto_201700404.Models.ViewModels;
using Proyecto_201700404.Clases;

namespace Proyecto_201700404.Controllers
{
    public class Contenedor_
    {
        public static List<MovimientoViewModel> movimientos = new List<MovimientoViewModel>();
        public static int turno=1;

        public static Casilla[,] tablero = new Casilla[8,8];
        //public static void llenar(int fila,int columna) {
        //    for (int i = 0; i < 8; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            tablero[i,j] = new Casilla();

        //        }
        //    } 

        //}
        public static Boolean VerificarExistencia(int fil, int col) {
            Boolean encontro = false;
            foreach (MovimientoViewModel item in movimientos)
            {
                if (item.fila==fil && item.col==col) {
                    encontro = true;
                    break;
                }

            }


            return encontro;
        }
        public static void Iniciarlista() {
            MovimientoViewModel mov = new MovimientoViewModel();
            MovimientoViewModel mov2 = new MovimientoViewModel();
            MovimientoViewModel mov3 = new MovimientoViewModel();
            MovimientoViewModel mov4 = new MovimientoViewModel();

            mov.fila = 4;
            mov.col = 4;
            mov.color = "blanco";

            mov2.fila = 5;
            mov2.col = 5;
            mov2.color = "blanco";

            mov3.fila = 4;
            mov3.col = 5;
            mov3.color = "negro";

            mov4.fila = 5;
            mov4.col = 4;
            mov4.color = "negro";

            movimientos.Add(mov);
            movimientos.Add(mov2);
            movimientos.Add(mov3);
            movimientos.Add(mov4);






        }

        
    }
}