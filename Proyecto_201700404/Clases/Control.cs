using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Control
    {
        List<Posibilidad> lista_posibilidades;

        public Control() {
            lista_posibilidades = new List<Posibilidad>();
        }
        public List<Posibilidad> listadePosibilidades() {
            return lista_posibilidades;
        }

        public void buscarXColor( int turno) {

            string color = "";
            if (turno == 1) {
                color = "negro";
            } else if (turno==2) {
                color = "blanco";
            
            }
            foreach (var item in Variables.fichas)
            {
                if (item.color==color) {
                    this.PosibilidadXDerecha(item);
                    this.PosibilidadXIzquierda(item);
                    this.posibilidadXAbajo(item);
                    this.buscarXArriba(item);
                    //this.diagonalDerechaInferior(item);
                    //this.diagonalIzquierdaInferior(item);
                    //this.diagonalSuperiorDerecha(item);
                    //this.diagonalSuperiorIzquierda(item);
                }
            }

        }
        public void mostraTabla() {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    System.Diagnostics.Debug.Write(Variables.tablero[i,j].estado+"       ");

                }
                System.Diagnostics.Debug.WriteLine("");

            }
        }

        public void PosibilidadXDerecha(Ficha fichaelegida)
        {//se ubicara en las celdas de las fichas de color encontradas
            //Console.WriteLine("Entro a buscar posibilidades por la derecha");

            Posibilidad posibilidad = new Posibilidad();
            int parar = 1;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++) //para recorrer el tablero
                {
                    if (i == (fichaelegida.fila - 1) && j > (fichaelegida.columna - 1)) //se ubica a la par de la casilla de la ficha que elegimos
                    {
                        if (Variables.tablero[i, j].estado == "ocupado")
                        { //si el de a la par esta ocupado, buscara el color de ficha que esta aca
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                break;
                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {

                                //Console.WriteLine("hay una ficha color: " + this.buscarFicha(i, j).color + " fila: " + this.buscarFicha(i, j).fila + "  columna: " + this.buscarFicha(i, j).columna);
                                //posibilidad.getLista().Add(this.buscarFicha(i,j));
                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                //System.Diagnostics.Debug.WriteLine(Variables.tablero[i,j].estado);
                                //System.Diagnostics.Debug.WriteLine("fila: "+i+" columna:  "+j );
                                

                                if ((j + 1) < 8) {
                                    //System.Diagnostics.Debug.WriteLine("el siguiente tiene estado:  " + Variables.tablero[i, j + 1].estado);
                                    if (Variables.tablero[i, j + 1] != null && Variables.tablero[i, j + 1].estado == "desocupado" && Variables.tablero[i, j - 1].estado == "ocupado")
                                    //si la casilla de a la par existe y esta desocupado                                
                                    {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 1;
                                        posibilidad.Columna = j + 2;
                                        //System.Diagnostics.Debug.WriteLine("hay una posibilidad en fila: "+posibilidad.Fila+"  columna: "+posibilidad.Columna);
                                        lista_posibilidades.Add(posibilidad);
                                        parar = 2;
                                        break;

                                    }

                                }

                                

                                


                            }



                        }
                        else if (Variables.tablero[i, j].estado == "desocupado" && (j - (fichaelegida.columna - 1)) == 1)//la casilla de a la par esta vacia, no hay posiblidad de juego alli
                        {
                            break;//no se hace nada
                        }

                    }
                }
                if (parar!=1) {
                    break;
                }

            }


        }

        public void PosibilidadXIzquierda(Ficha fichaelegida)

        {

            int parar = 1;
            Posibilidad posibilidad = new Posibilidad();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (i == (fichaelegida.fila - 1) && j < (fichaelegida.columna - 1))
                    { //ubico la ficha que esta a la izquierda de la elegida
                        if (Variables.tablero[i, j].estado == "ocupado")
                        { //si el de a la par esta ocupado, buscara el color de ficha que esta aca

                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                break;
                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {
                                //    Console.WriteLine("hay una ficha color: " + this.buscarFicha(i, j).color + " fila: " + this.buscarFicha(i, j).fila + "  columna: " + this.buscarFicha(i, j).columna);

                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if ((j-1)>=0) {

                                    if (Variables.tablero[i, j - 1] != null && Variables.tablero[i, j - 1].estado == "desocupado" && Variables.tablero[i, j + 1].estado == "ocupado")
                                    //si la casilla de a la par existe y esta desocupado                                
                                    {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 1;
                                        posibilidad.Columna = j;
                                        lista_posibilidades.Add(posibilidad);
                                        parar = 2;
                                        break;

                                    }

                                }
                                
                                


                            }



                        }
                        else if (Variables.tablero[i, j].estado == "desocupado" && (j - (fichaelegida.columna - 1)) == -1)//la casilla de a la par esta vacia, no hay posiblidad de juego alli
                        {
                            break;//no se hace nada
                        }

                    }

                }
                if (parar!=1) {
                    break;
                }

            }
        }


        public void posibilidadXAbajo(Ficha fichaelegida)
        {

            int parar = 1;
            Posibilidad posibilidad = new Posibilidad();
            int auxfila = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == (auxcol) && i > (auxfila))
                    {//esta una casilla por debajo

                        if (Variables.tablero[i, j].estado == "ocupado")
                        {
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                //si la ficha de abajo es del mismo color el proceso para
                                break;
                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {


                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if ((i+1)<8) {
                                    if (Variables.tablero[i + 1, j] != null && Variables.tablero[i + 1, j].estado == "desocupado" && Variables.tablero[i - 1, j].estado == "ocupado")
                                    {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i + 2;
                                        posibilidad.Columna = j + 1;
                                        lista_posibilidades.Add(posibilidad);
                                        parar = 2;
                                        break;
                                    }


                                }
                                

                            }

                        }
                        else if (Variables.tablero[i, j].estado == "desocupado" && (i - auxfila) == 1 && (j - (auxcol)) == 0)
                        { //si la casilla de abajo esta desocupada el proceso para
                            //Console.WriteLine("ENTRO AL BRAKE");
                            break;

                        }


                    }
                }
                if (parar!=1) {
                    break;
                }
            }
        }


        public void buscarXArriba(Ficha fichaelegida)
        {
            int parar = 1;
            Posibilidad posibilidad = new Posibilidad();
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == (fichaelegida.columna - 1) && i < (fichaelegida.fila - 1))
                    { //se ubica arriba de la ficha que elegimos
                        if (Variables.tablero[i, j].estado == "ocupado")
                        {
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                break;
                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {

                                //Console.WriteLine("hay una ficha color: " + this.buscarFicha(i, j).color + " fila: " + this.buscarFicha(i, j).fila + "  columna: " + this.buscarFicha(i, j).columna);
                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if ((i-1)>=0) {
                                    if (Variables.tablero[i - 1, j] != null && Variables.tablero[i - 1, j].estado == "desocupado" & Variables.tablero[i + 1, j].estado == "ocupado")
                                    {
                                        posibilidad.origen = fichaelegida;
                                        posibilidad.Fila = i;
                                        posibilidad.Columna = j + 1;
                                        lista_posibilidades.Add(posibilidad);
                                        parar = 2;
                                        break;
                                    }


                                }
                                

                            }

                        }
                        else if (Variables.tablero[i, j].estado == "desocupado" && (i - (fichaelegida.fila - 1)) == -1)
                        {
                            break;

                        }

                    }

                }
                if (parar!=1) {
                    break;
                }

            }

        }


        public void diagonalDerechaInferior(Ficha fichaelegida)
        {
            int auxfila = fichaelegida.fila - 1;
            int auxcol = fichaelegida.columna - 1;
            int frenar = 1;
            Posibilidad posibilidad = new Posibilidad();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i > auxfila && j > auxcol)
                    {

                        if ((i - auxfila) == 1 && (j - auxcol) == 1)
                        {
                            auxfila = i;
                            auxcol = j;
                        }
                        //Console.WriteLine("entro con ficha fila: " + i + " columan: " + j);

                        if (Variables.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else if (Variables.tablero[i, j].estado == "ocupado")
                        {
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                frenar = 2;
                                break;
                            }


                            if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {
                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if (Variables.tablero[i + 1, j + 1] != null && Variables.tablero[i + 1, j + 1].estado == "desocupado")
                                {


                                    posibilidad.origen = fichaelegida;
                                    posibilidad.Fila = i + 2;
                                    posibilidad.Columna = j + 2;
                                    lista_posibilidades.Add(posibilidad);
                                    frenar = 2;
                                }


                            }
                            //    Console.WriteLine("entro con ficha fila: " + i + " columan: " + j);
                            //    if (Program.tablero[i + 1, j + 1] != null && Program.tablero[i + 1, j + 1].estado == "desocupado") {


                            //        Posibilidad posibilidad = new Posibilidad();
                            //        posibilidad.origen = fichaelegida;
                            //        posibilidad.Fila = i + 2;
                            //        posibilidad.Columna = j + 2;
                            //        lista_posibilidades.Add(posibilidad);
                            //        frenar = 2;

                            //    }

                            //} else if(fichaelegida.color==this.buscarFicha(i,j).color){
                            //    frenar = 2;
                            //    break;

                            //}

                            //if (fichaelegida.color==this.buscarFicha(i,j).color) {
                            //    break;
                            //}


                        }

                        //    if (Program.tablero[i, j].estado == "desocupado") {
                        //        break;
                        //    }
                        //    else if (Program.tablero[i, j].estado == "ocupado") {
                        //        if (fichaelegida.color == this.buscarFicha(i, j).color) {
                        //            break;
                        //        }
                        //        else if (fichaelegida.color!=this.buscarFicha(i,j).color) {
                        //            if (Program.tablero[i+1,j+1]!=null && Program.tablero[i+1,j+1].estado=="desocupado") {
                        //                Posibilidad posibilidad = new Posibilidad();
                        //                posibilidad.Fila = i + 2;
                        //                posibilidad.Columna = j + 2;
                        //                lista_posibilidades.Add(posibilidad);

                        //            }
                        //        }
                        //    }
                    }
                    if (frenar != 1)
                    {
                        break;
                    }

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

            for (int i = 0; i < 8; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (i > auxfila && j < auxcol)
                    {

                        if ((i - auxfila) == 1 && (j - auxcol) == -1)
                        {
                            auxfila = i;
                            auxcol = j;
                        }


                        if (Variables.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else if (Variables.tablero[i, j].estado == "ocupado")
                        {
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                frenar = 2;
                                break;
                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {

                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if (Variables.tablero[i + 1, j - 1] != null && Variables.tablero[i + 1, j - 1].estado == "desocupado")
                                {
                                    //Console.WriteLine(" paso aca entro a fila: " + i + "  columna: " + j);

                                    posibilidad.origen = fichaelegida;
                                    posibilidad.Fila = i + 2;
                                    posibilidad.Columna = j;
                                    lista_posibilidades.Add(posibilidad);


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

            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i < auxfila && j > auxcol)
                    {

                        if ((i - auxfila) == -1 && (j - auxcol) == 1)
                        {
                            auxcol = j;
                            auxfila = i;
                        }




                        if (Variables.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else if (Variables.tablero[i, j].estado == "ocupado")
                        {
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                frenar = 2;
                                break;

                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {
                                //Console.WriteLine(" paso aca entro a fila: " + i + "  columna: " + j);
                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if (Variables.tablero[i - 1, j + 1] != null && Variables.tablero[i - 1, j + 1].estado == "desocupado")
                                {

                                    posibilidad.origen = fichaelegida;
                                    posibilidad.Fila = i;
                                    posibilidad.Columna = j + 2;
                                    lista_posibilidades.Add(posibilidad);

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
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (i < auxfil && j < auxcol)
                    {

                        if ((i - auxfil) == -1 && (j - auxcol) == -1)
                        {
                            auxfil = i;
                            auxcol = j;
                        }

                        if (Variables.tablero[i, j].estado == "desocupado")
                        {
                            frenar = 2;
                            break;
                        }
                        else if (Variables.tablero[i, j].estado == "ocupado")
                        {
                            if (fichaelegida.color == this.buscarFicha(i, j).color)
                            {
                                frenar = 2;
                                break;
                            }
                            else if (fichaelegida.color != this.buscarFicha(i, j).color)
                            {

                                posibilidad.getLista().Add(this.buscarFicha(i, j));
                                if (Variables.tablero[i - 1, j - 1] != null && Variables.tablero[i - 1, j - 1].estado == "desocupado")
                                {

                                    posibilidad.origen = fichaelegida;
                                    posibilidad.Fila = i;
                                    posibilidad.Columna = j;
                                    lista_posibilidades.Add(posibilidad);

                                }
                            }
                            //Console.WriteLine(" paso aca entro a fila: " + i + "  columna: " + j);
                        }


                        //if (Program.tablero[i, j].estado == "ocupado") {

                        //    if (fichaelegida.color == this.buscarFicha(i, j).color) {
                        //        break;
                        //    }
                        //    else if (fichaelegida.color!=this.buscarFicha(i,j).color) {

                        //        if ((j-auxcol)==-1 && (i-auxfil)==-1) {
                        //            if (Program.tablero[i - 1, j - 1] != null && Program.tablero[i - 1, j - 1].estado == "desocupado" && Program.tablero[i + 1, j + 1].estado == "ocupado" && this.buscarFicha(i + 1, j + 1).color != this.buscarFicha(i, j).color)
                        //            {
                        //                Posibilidad posibilidad = new Posibilidad();
                        //                posibilidad.origen = fichaelegida;
                        //                posibilidad.Fila = i;
                        //                posibilidad.Columna = j;
                        //                lista_posibilidades.Add(posibilidad);


                        //            }
                        //            else if ((j-auxcol)<-1 && (i-auxfil)<-1) {
                        //                if (Program.tablero[i-1,j-1]!=null && Program.tablero[i-1,j-1].estado=="desocupado" && Program.tablero[i+1,j+1].estado=="ocupado" && this.buscarFicha(i+1,j+1).color==this.buscarFicha(i,j).color) {
                        //                    Posibilidad posibilidad = new Posibilidad();
                        //                    posibilidad.origen = fichaelegida;
                        //                    posibilidad.Fila = i;
                        //                    posibilidad.Columna = j;
                        //                    lista_posibilidades.Add(posibilidad);

                        //                }
                        //            }


                        //        }


                        //    }

                        //}
                        //else if (Program.tablero[i,j].estado=="desocupado" && (i-auxfil)==-1 && (j-auxcol)==-1) {
                        //    break;
                        //}

                    }



                }
                if (frenar != 1)
                {
                    break;
                }

            }


        }
        public void buscarfichaParavoltear(Ficha fichaelegida) {//la ficha que se coloco, pertenece a las posibilidades de juego

            foreach (var item in lista_posibilidades)
            {
                if (item.Fila==fichaelegida.fila && item.Columna==fichaelegida.columna) {//se ubica en la ficha que se agreggo
                    foreach (var item2 in item.getLista())//busca la fichas que volteara esa posibilidad
                    {
                        this.voltearFichas(item2, fichaelegida);

                    }
                }

            }
        
        }
        public void voltearFichas(Ficha fichaavoltear,Ficha fichacolocada) {
            foreach (var item in Variables.fichas)
            {
                if (item.columna==fichaavoltear.columna && item.fila==fichaavoltear.fila) {
                    if (item.color!=fichacolocada.color) {
                        item.color = fichacolocada.color;
                    }
                    
                }

            }
            
        }




        public Ficha buscarFicha(int fila, int col)
        {
            Ficha ficha = new Ficha();

            //Console.WriteLine("datos de entrada fila: "+fila+" columna: "+col);
            foreach (var item in Variables.fichas)
            {
                //Console.WriteLine("info de fichas, fila: "+item.fila+" columna: "+item.columna+" color:  "+item.color);
                if (item.fila == (fila + 1) && item.columna == (col + 1))
                {
                    ficha = item;
                    break;
                }
            }
            //Console.WriteLine("fila: "+ficha.fila+"  columna: "+ficha.columna);
            return ficha;

        }


    }
}