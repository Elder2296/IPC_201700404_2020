using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Variables
    {
        public static List<Ficha> fichas = new List<Ficha>();
        public static Casilla[,] tablero = new Casilla[8, 8];
        public static int valides = 0;
        public static int mov_jugador1 = 0;
        public static int mov_jugador2 = 0;


        public static int turno = 0;

        public static void fichasbasicas() {
            Ficha ficha1 = new Ficha();
            Ficha ficha2 = new Ficha();
            Ficha ficha3 = new Ficha();
            Ficha ficha4 = new Ficha();
            //Ficha ficha5 = new Ficha();
            //Ficha ficha6 = new Ficha();
            //Ficha ficha7 = new Ficha();
            //Ficha ficha8 = new Ficha();
            //Ficha ficha9 = new Ficha();
            //Ficha ficha10 = new Ficha();
            //Ficha ficha11 = new Ficha();

            ficha1.color = "blanco";
            ficha1.columna = 4;
            ficha1.fila = 4;

            ficha2.color = "blanco";
            ficha2.columna = 5;
            ficha2.fila = 5;

            ficha3.color = "negro";
            ficha3.columna = 5;
            ficha3.fila = 4;

            ficha4.color = "negro";
            ficha4.columna = 4;
            ficha4.fila = 5;

            //ficha5.color = "blanco";
            //ficha5.columna = 6;
            //ficha5.fila = 5;

            //ficha6.color = "blanco";
            //ficha6.columna = 7;
            //ficha6.fila = 6;

            //ficha7.color = "blanco";
            //ficha7.columna = 3;
            //ficha7.fila = 6;

            //ficha8.color = "blanco";
            //ficha8.columna = 6;
            //ficha8.fila = 3;

            //ficha9.color = "negro";
            //ficha9.columna = 3;
            //ficha9.fila = 3;

            //ficha10.color = "blanco";
            //ficha10.columna = 2;
            //ficha10.fila = 7;

            //ficha11.color = "negro";
            //ficha11.columna = 2;
            //ficha11.fila = 2;

            fichas.Add(ficha1);
            fichas.Add(ficha2);
            fichas.Add(ficha3);
            fichas.Add(ficha4);
            //fichas.Add(ficha5);
            //fichas.Add(ficha6);
            //fichas.Add(ficha7);
            //fichas.Add(ficha8);
            //fichas.Add(ficha9);
            //fichas.Add(ficha10);
            iniciarTablero();



        }
        public static void iniciarTablero() {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int r = 0;
                    foreach (var item in fichas)
                    {
                        if (i==item.fila-1 &&  j==item.columna-1) {
                            tablero[i, j] = new Casilla();
                            tablero[i, j].estado = "ocupado";
                            r = 1;
                            break;


                        }

                        

                    }
                    if (r==0) {
                        tablero[i, j] = new Casilla();
                        tablero[i, j].estado = "desocupado";
                    }

                }

            }
        }

        public static void actualizarTablero(Ficha ficha) {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(i==(ficha.fila-1) && j==(ficha.columna-1)){
                        tablero[i, j].estado = "ocupado";
                    }

                }

            }


        }
    }
}