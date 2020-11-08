using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto_201700404.Clases;

namespace Proyecto_201700404.Clases
{
    public class Validar
    {
        List<Posibilidad> lista_posibilidades;
        Casilla[,] tablero;
        List<Ficha> fichaslocal;
        int filas, columnas;
        public Validar(List<Ficha> fichas,int filas,int columnas) {
            this.filas = filas;
            this.columnas = columnas;
            this.lista_posibilidades = new List<Posibilidad>();
            this.tablero = new Casilla[filas, columnas];
            this.iniciarTablero(filas,columnas);//inicia matriz de desocupados
            this.iniciarFichasLocal(fichas);//inicia una lista de fichas igual al de las original de partida
            this.llenartablero(this.filas,this.columnas);//cambia los estados de algunas casillas que estan ocupadas
        }
        public List<Posibilidad> listadePosibilidades()
        {
            return lista_posibilidades;
        }
        public List<Ficha> getFichasLocal() {
            return this.fichaslocal;
        }
        public void Buscar(int turno) {
            foreach (var item in this.fichaslocal)
            {
                if (item.Turnodueño==turno) {
                    this.posibilidadXDerecha(item);
                    this.posibilidadXIzquierda(item);
                    this.posibilidadXAbajo(item);
                    this.posibilidadXArriba(item);
                    this.diagonalDerechaInferior(item);
                    this.diagonalIzquierdaInferior(item);
                    this.diagonalSuperiorDerecha(item);
                    this.diagonalSuperiorIzquierda(item);
                }

            }
        }
        public void posibilidadXDerecha(Ficha fichaelegida) {
            Posibilidad posibilidad = new Posibilidad();
            int parar = 1;

            for (int i = 0; i < this.filas; i++)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    if (i==(fichaelegida.fila-1) && j>(fichaelegida.columna-1)) {
                        if (this.tablero[i,j].estado=="ocupado") {
                            if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño) {
                                parar = 0;//se parara el proceso si el que esta a la par es una ficha del mismo usuario
                                break;
                            }
                            else {
                                posibilidad.getLista().Add(this.buscarFicha(i,j));

                                if ((j+1)<columnas) {

                                    if (this.tablero[i,j+1].estado=="desocupado" && this.tablero[i,j-1].estado=="ocupado") {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 1;
                                        posibilidad.Columna = j + 2;
                                        lista_posibilidades.Add(posibilidad);
                                        parar = 0;
                                        break;
                                    }

                                }

                            }
                            
                        }
                    

                    }

                }
                if (parar!=1) {
                    break;
                }

            }

        
        }
        public void posibilidadXIzquierda(Ficha fichaelegida) {
            int parar = 1;
            Posibilidad posibilidad = new Posibilidad();
            for (int i = 0; i < this.filas; i++)
            {
                for (int j = (this.columnas-1); j >= 0; j--)
                {
                    if (i==(fichaelegida.fila-1) && j<(fichaelegida.columna-1)) {

                        if (this.tablero[i,j].estado=="ocupado") {
                            if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                            {
                                parar = 0;
                                break;
                            }
                            else {
                                posibilidad.getLista().Add(this.buscarFicha(i,j));
                                if ((j-1)>=0) {
                                    if (this.tablero[i, j - 1].estado == "desocupado" && this.tablero[i,j+1].estado=="ocupado") {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 1;
                                        posibilidad.Columna = j;
                                        this.lista_posibilidades.Add(posibilidad);
                                        parar = 0;
                                        break;
                                    
                                    }
                                }

                            }
                        }
                    }

                }
                if (parar!=1) {
                    break;
                }

            }

        }

        public void posibilidadXAbajo(Ficha fichaelegida) {
            int parar = 1;
            Posibilidad posibilidad = new Posibilidad();
            int auxfila = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;

            for (int i = 0; i < this.filas; i++)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    if (j==auxcol && i>auxfila) {
                        if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                        {
                            parar = 0;
                            break;
                        }
                        else {
                            posibilidad.getLista().Add(this.buscarFicha(i,j));
                            if ((i+1)<this.filas) {
                                if (this.tablero[i+1,j].estado=="desocupado" && this.tablero[i-1,j].estado=="ocupado") {
                                    posibilidad.origen = fichaelegida;
                                    posibilidad.Fila = i + 2;
                                    posibilidad.Columna = j + 1;
                                    this.lista_posibilidades.Add(posibilidad);
                                    parar = 0;
                                    break;
                                }
                            }
                        }
                    }

                }
                if (parar!=1) {
                    break;
                }

            }
        }
        public void posibilidadXArriba(Ficha fichaelegida) {
            int parar = 1;
            Posibilidad posibilidad = new Posibilidad();
            for (int i = (this.filas-1); i >=0 ; i--)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    if (j==(fichaelegida.columna-1)&& i<(fichaelegida.fila-1)) {
                        if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                        {
                            parar = 0;
                            break;
                        }
                        else {
                            posibilidad.getLista().Add(this.buscarFicha(i,j));
                            if ((i - 1) >= 0) {
                                if (this.tablero[i-1,j].estado=="desocupado" && this.tablero[i+1,j].estado=="ocupado") {
                                    posibilidad.origen = fichaelegida;
                                    posibilidad.Fila = i;
                                    posibilidad.Columna = j + 1;
                                    this.lista_posibilidades.Add(posibilidad);
                                    parar = 0;
                                    break;
                                }
                            }
                        }
                    }

                }
                if (parar!=1) {
                    break;
                }

            }
        }

        public void diagonalDerechaInferior(Ficha fichaelegida) {
            int auxfila = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;
            int frenar = 1;
            Posibilidad posibilidad = new Posibilidad();

            for (int i = 0; i < this.filas; i++)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    if (i>auxfila & j>auxcol) {
                        if ((i-auxfila)==1 && (j-auxcol)==1) {
                            auxfila = i;
                            auxcol = j;
                        }

                        if (this.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 0;
                            break;
                        }
                        else {
                            if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                            {
                                frenar = 0;
                                break;
                            }
                            else {
                                posibilidad.getLista().Add(this.buscarFicha(i,j));

                                if ((i+1)<this.filas && (j+1)<this.columnas) {
                                    if (this.tablero[i+1,j+1].estado=="desocupado") {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 2;
                                        posibilidad.Columna = j + 2;
                                        this.lista_posibilidades.Add(posibilidad);
                                        frenar = 0;
                                        break;
                                    }
                                }
                            }

                            
                        }
                    }

                }
                if (frenar!=1) {
                    break;
                }

            }
        }

        public void diagonalIzquierdaInferior(Ficha fichaelegida)
        {
            int auxfila = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;
            int frenar = 1;
            //Console.WriteLine("PASO ACA");
            Posibilidad posibilidad = new Posibilidad();

            for (int i = 0; i < this.filas; i++)
            {
                for (int j = (this.columnas-1); j >= 0; j--)
                {
                    if (i > auxfila && j < auxcol)
                    {

                        if ((i - auxfila) == 1 && (j - auxcol) == -1)
                        {
                            auxfila = i;
                            auxcol = j;
                        }


                        if (this.tablero[i,j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else 
                        {
                            if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                            {
                                frenar = 2;
                                break;
                            }
                            else
                            {

                                posibilidad.getLista().Add(this.buscarFicha(i, j));

                                if ((i + 1) < this.filas && (j - 1) >= 0)
                                {

                                    if ( this.tablero[i + 1, j - 1].estado == "desocupado")
                                    {
                                        //Console.WriteLine(" paso aca entro a fila: " + i + "  columna: " + j);

                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 2;
                                        posibilidad.Columna = j;
                                        lista_posibilidades.Add(posibilidad);
                                        frenar = 2;
                                        break;


                                    }


                                }


                            }

                        }



                    }

                }
                if (frenar != 1)
                {
                    break;
                }

            }

        }



        public void diagonalSuperiorDerecha(Ficha fichaelegida)
        {
            int auxfila = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;
            int frenar = 1;
            Posibilidad posibilidad = new Posibilidad();

            for (int i = (this.filas-1); i >= 0; i--)
            {
                for (int j = 0; j < this.columnas; j++)
                {
                    if (i < auxfila && j > auxcol)
                    {

                        if ((i - auxfila) == -1 && (j - auxcol) == 1)
                        {
                            auxcol = j;
                            auxfila = i;
                        }




                        if (this.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else
                        {
                            if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                            {
                                frenar = 2;
                                break;

                            }
                            else 
                            {
                                //Console.WriteLine(" paso aca entro a fila: " + i + "  columna: " + j);
                                posibilidad.getLista().Add(this.buscarFicha(i, j));

                                if ((i - 1) >= 0 && (j + 1) < this.columnas)
                                {
                                    if ( this.tablero[i - 1, j + 1].estado == "desocupado")
                                    {

                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i;
                                        posibilidad.Columna = j + 2;
                                        lista_posibilidades.Add(posibilidad);
                                        frenar = 2;
                                        break;

                                    }



                                }



                            }

                        }





                    }


                }
                if (frenar != 1)
                {
                    break;
                }

            }



        }





        public void diagonalSuperiorIzquierda(Ficha fichaelegida)
        {
            int auxfil = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;
            int frenar = 1;
            Posibilidad posibilidad = new Posibilidad();
            for (int i = (this.filas-1); i >= 0; i--)
            {
                for (int j = (this.columnas-1); j >= 0; j--)
                {
                    if (i < auxfil && j < auxcol)
                    {

                        if ((i - auxfil) == -1 && (j - auxcol) == -1)
                        {
                            auxfil = i;
                            auxcol = j;
                        }

                        if (this.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else 
                        {
                            if (fichaelegida.Turnodueño == this.buscarFicha(i, j).Turnodueño)
                            {
                                frenar = 2;
                                break;
                            }
                            else 
                            {

                                posibilidad.getLista().Add(this.buscarFicha(i, j));


                                if ((i - 1) >= 0 && (j - 1) >= 0)
                                {
                                    if ( this.tablero[i - 1, j - 1].estado == "desocupado")
                                    {

                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i;
                                        posibilidad.Columna = j;
                                        lista_posibilidades.Add(posibilidad);
                                        frenar = 2;
                                        break;

                                    }



                                }

                            }
                           
                        }


                    }



                }
                if (frenar != 1)
                {
                    break;
                }

            }


        }














        public void buscarFichaParaVoltear(Ficha fichaelegida)
        {//la ficha que se coloco, pertenece a las posibilidades de juego
            foreach (var item in this.lista_posibilidades)
            {
                if (item.Fila==fichaelegida.fila && item.Columna==fichaelegida.columna) {

                    foreach (var item2 in item.getLista())
                    {
                        this.voltearFichas(item2,fichaelegida);

                    }
                }
                    

            }
        }

        public void voltearFichas(Ficha fichaavoltear,Ficha fichacolocada) {

            foreach (var item in this.fichaslocal)
            {
                if (item.columna==fichaavoltear.columna && item.fila==fichaavoltear.fila) {
                    item.color = fichacolocada.color;
                    item.clase = fichacolocada.clase;
                    item.Turnodueño = fichacolocada.Turnodueño;
                }

            }
        }

        public void iniciarFichasLocal(List<Ficha> fichas) {
            this.fichaslocal = new List<Ficha>();

            foreach (var item in fichas)
            {
                this.fichaslocal.Add(item);

            }
        }

        public Ficha buscarFicha(int fila, int col) {
            Ficha  ficha= new Ficha();
            foreach (var item in this.fichaslocal)
            {
                if (item.fila==(fila+1)&& item.columna==(col+1)) {
                    ficha = item;
                    break;
                }

            }
            return ficha;
            
        }
        public void iniciarTablero(int fil,int col) {
            for (int i = 0; i < fil; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.tablero[i, j] = new Casilla();
                    this.tablero[i, j].estado = "desocupado";


                }

            }
            
        }

        public void llenartablero(int filas,int columnas) {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    foreach (var item in this.fichaslocal)
                    {
                        if (i==(item.fila-1) && j== (item.columna-1)) {
                            this.tablero[i, j].estado = "ocupado";
                            
                        }

                    }

                }

            }
            

        
        }


    }
}