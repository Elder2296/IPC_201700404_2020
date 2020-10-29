using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Partida
    {
        private Jugador local;
        private Jugador invitado;
        private Casilla[,] tablero;
        private int FilasTablero;
        private int ColumnasTablero;
        private int turnojugador;
        private List<Ficha> fichas;
        public Partida() {
            this.turnojugador = 1;
        
        }
        
        public void IniciarPartida() {
            local = new Jugador();
            invitado = new Jugador();
            fichas = new List<Ficha>();

        
        }
        public List<Ficha> getListaFichas() {
            return this.fichas;
        }
        public Jugador JugadorLocal() {
            return local;
        }
        public Jugador JugadorInvitado() {
            return invitado;
        }
        public int getFilasTablero() {
            return this.FilasTablero;
        }
        public int getColumnasTabler() {
            return this.ColumnasTablero;
        }
        public void IniciarTablero(int filas, int columnas) {
            this.FilasTablero = filas;
            this.ColumnasTablero = columnas;
            tablero = new Casilla[filas, columnas];

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    tablero[i,j] = new Casilla();
                    tablero[i, j].estado = "desocupado";


                }

            }

        }
        public void PonerFicha(int fil, int col) {
            Ficha ficha = new Ficha();
            Ficha aux = new Ficha();
            ficha.fila = fil;
            ficha.columna = col;

            if (this.turnojugador == 1) {
                aux = this.JugadorLocal().ficha_a_Colocar();
                ficha.color = aux.color;
                ficha.clase = aux.clase;

                System.Diagnostics.Debug.WriteLine("Ficha COLOR: "+ficha.color+" CLASE: "+ficha.clase+" FILA: "+ficha.fila+" COLUMNA: "+ficha.columna);

                this.fichas.Add(ficha);

                //this.fichas.Last().fila = fil;
                //this.fichas.Last().columna = col;
                this.ActualizarTablero(ficha);
                this.turnojugador = 2;

            } else if (this.turnojugador==2) {
                aux = this.JugadorInvitado().ficha_a_Colocar();

                ficha.color = aux.color;
                ficha.clase = aux.clase;

                System.Diagnostics.Debug.WriteLine("Ficha COLOR: " + ficha.color + " CLASE: " + ficha.clase + " FILA: " + ficha.fila + " COLUMNA: " + ficha.columna);


                this.fichas.Add(ficha);
                //this.fichas.Last().fila = fil;
                //this.fichas.Last().columna = col;
                this.ActualizarTablero(ficha);
                
                this.turnojugador = 1;
            }
        }

        public void ActualizarTablero(Ficha ficha) {
            for (int i = 0; i < this.FilasTablero; i++)
            {
                for (int j = 0; j < this.ColumnasTablero; j++)
                {
                    if (i==ficha.fila-1 && j==ficha.columna-1) {
                        this.tablero[i, j].estado = "ocupado";
                    }

                }

            }
            
        }
    }
}