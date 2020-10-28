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

        public int turno { set; get; }
        
        public void IniciarPartida() {
            local = new Jugador();
            invitado = new Jugador();

        
        }
        public Jugador JugadorLocal() {
            return local;
        }
        public Jugador JugadorInvitado() {
            return invitado;
        }
        public void IniciarTablero(int filas, int columnas) {
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
    }
}