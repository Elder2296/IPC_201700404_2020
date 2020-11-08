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
        public Casilla[,] getTablero() {
            return this.tablero;
        }
        public void IniciarPartida() {
            local = new Jugador();
            invitado = new Jugador();
            fichas = new List<Ficha>();
           

        }
        public List<Ficha> getListaFichas() {
            return this.fichas;
        }
        public int getTurno() {
            return this.turnojugador;
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
                    tablero[i, j] = new Casilla();
                    tablero[i, j].estado = "desocupado";


                }

            }

        }
        public void PonerFicha(int fil, int col)
        {
            Ficha ficha = new Ficha();
            Ficha aux = new Ficha();
            ficha.fila = fil;
            ficha.columna = col;
            System.Diagnostics.Debug.WriteLine("LA LISTA DE FICHAS TIENE TAMAÑO: " + this.fichas.Count());
            if (this.turnojugador == 1)
            {
                aux = this.JugadorLocal().ficha_a_Colocar();
                ficha.color = aux.color;
                ficha.clase = aux.clase;
                ficha.Turnodueño = aux.Turnodueño;

                //System.Diagnostics.Debug.WriteLine("Ficha COLOR: "+ficha.color+" CLASE: "+ficha.clase+" FILA: "+ficha.fila+" COLUMNA: "+ficha.columna+" TURNO DUEÑO: "+ficha.Turnodueño);

                if (this.fichas.Count() < 4)
                {
                    this.fichas.Add(ficha);
                    this.ActualizarTablero(ficha);
                    this.JugadorLocal().Movimientos();

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ENTRO ACA ESTA MIERDA");
                    Validar validacion = new Validar(this.fichas, this.FilasTablero, this.ColumnasTablero);// hasta aca la validacion tiene su propio tablero de casillas y su propia lista
                    validacion.Buscar(this.turnojugador);
                    int encontro = 0;
                    foreach (var item in validacion.listadePosibilidades())
                    {
                        System.Diagnostics.Debug.WriteLine("HAY POSIBILIDAD EN FILA: " + item.Fila + " COLUMNA: " + item.Columna);

                        if (item.Fila == ficha.fila && item.Columna == ficha.columna)
                        {
                            encontro = 1;
                        }


                    }
                    if (encontro != 0)
                    {
                        validacion.buscarFichaParaVoltear(ficha);
                        this.fichas.Clear();
                        foreach (var item in validacion.getFichasLocal())
                        {
                            this.fichas.Add(item);

                        }

                        this.fichas.Add(ficha);
                        Variables.valides = 1;
                        Variables.validesde = this.JugadorLocal().Alias;
                        this.JugadorLocal().Movimientos();
                        this.tablero[ficha.fila - 1, ficha.columna - 1].estado = "ocupado";
                    }
                    else {
                        Variables.valides = 2;
                        Variables.validesde = this.JugadorLocal().Alias;

                    }



                }



                //this.fichas.Last().fila = fil;
                //this.fichas.Last().columna = col;

                this.turnojugador = 2;

                if (this.fichas.Count() < 4)
                {
                }
                else {
                    Validar validar = new Validar(this.fichas, this.FilasTablero, this.ColumnasTablero);// hasta aca la validacion tiene su propio tablero de casillas y su propia lista
                    validar.Buscar(this.turnojugador);

                    if (validar.listadePosibilidades().Count() == 0)
                    {
                        this.turnojugador = 1;
                        //Desde Aca debo mandar los datos de a quien le toca el turno
                    }
                    else
                    {
                        this.turnojugador = 2;
                    }
                }
                


            }
            else if (this.turnojugador == 2)
            {
                aux = this.JugadorInvitado().ficha_a_Colocar();

                ficha.color = aux.color;
                ficha.clase = aux.clase;
                ficha.Turnodueño = aux.Turnodueño;

                //System.Diagnostics.Debug.WriteLine("Ficha COLOR: " + ficha.color + " CLASE: " + ficha.clase + " FILA: " + ficha.fila + " COLUMNA: " + ficha.columna + " TURNO DUEÑO: " + ficha.Turnodueño);


                //this.fichas.Add(ficha);
                ////this.fichas.Last().fila = fil;
                ////this.fichas.Last().columna = col;
                //this.ActualizarTablero(ficha);



                if (this.fichas.Count() < 4)
                {
                    this.fichas.Add(ficha);
                    this.ActualizarTablero(ficha);
                    this.JugadorInvitado().Movimientos();

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ENTRO ACA ESTA MIERDA");
                    Validar validacion = new Validar(this.fichas, this.FilasTablero, this.ColumnasTablero);// hasta aca la validacion tiene su propio tablero de casillas y su propia lista
                    validacion.Buscar(this.turnojugador);
                    int encontro = 0;
                    foreach (var item in validacion.listadePosibilidades())
                    {
                        //System.Diagnostics.Debug.WriteLine("HAY POSIBILIDAD EN FILA: " + item.Fila + " COLUMNA: " + item.Columna);

                        if (item.Fila == ficha.fila && item.Columna == ficha.columna)
                        {
                            encontro = 1;
                        }


                    }
                    if (encontro != 0)
                    {
                        validacion.buscarFichaParaVoltear(ficha);
                        this.fichas.Clear();
                        foreach (var item in validacion.getFichasLocal())
                        {
                            this.fichas.Add(item);

                        }

                        this.fichas.Add(ficha);
                        this.JugadorInvitado().Movimientos();
                        Variables.valides = 1;
                        Variables.validesde = this.JugadorInvitado().Alias;
                        this.tablero[ficha.fila - 1, ficha.columna - 1].estado = "ocupado";
                    }
                    else {
                        Variables.valides = 2;
                        Variables.validesde = this.JugadorInvitado().Alias;
                    }



                }
                this.turnojugador = 1;

                if (this.fichas.Count() < 4)
                {
                }
                else {
                    Validar validar = new Validar(this.fichas, this.FilasTablero, this.ColumnasTablero);// hasta aca la validacion tiene su propio tablero de casillas y su propia lista
                    validar.Buscar(this.turnojugador);

                    if (validar.listadePosibilidades().Count() == 0)
                    {
                        this.turnojugador = 2;
                        //Desde Aca debo mandar los datos de a quien le toca el turno
                    }
                    else
                    {
                        this.turnojugador = 1;


                    }
                }


                
                System.Diagnostics.Debug.WriteLine("LA LISTA DE FICHAS TIENE TAMAÑO ACTUALIZADO: " + this.fichas.Count());
            }
        }

            public void ActualizarTablero(Ficha ficha) {
                for (int i = 0; i < this.FilasTablero; i++)
                {
                    for (int j = 0; j < this.ColumnasTablero; j++)
                    {
                        if (i == ficha.fila - 1 && j == ficha.columna - 1) {
                            this.tablero[i, j].estado = "ocupado";
                        }

                    }

                }

            }
        }
    }