using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Jugador
    {
        //public int identificador { set; get; }
        private int turnoficha;
        private int movimientos;
        public string Alias { set; get; }
        
        List<Ficha> misfichas;
        public Jugador() { 
            misfichas= new List<Ficha>();
            this.turnoficha = 0;
            this.movimientos = 0;
        }
        public void Movimientos() {
            this.movimientos = this.movimientos + 1;
        }
        public int getMovimientos() {
            return this.movimientos;
        }

        public Ficha ficha_a_Colocar() {
            Ficha ficha;

            if (this.turnoficha > this.misfichas.Count()-1)
            {
                this.turnoficha = 0;
            }
            
            ficha=this.misfichas.ElementAt(this.turnoficha);

            this.turnoficha = this.turnoficha + 1;
            return ficha;
            


        }
        public Ficha getFichaTurno() {
            Ficha ficha;
            ficha = this.misfichas.ElementAt(this.turnoficha);
            return ficha;
        }
        public List<Ficha> MisFichas() {
            return this.misfichas;
        }
        public Boolean puedoAgregarfichas() {
            Boolean sisepuede = false;
            if (this.misfichas.Count()<5) {
                sisepuede = true;
            }
            return sisepuede;
        }
        public Boolean fichaRepetida(Ficha ficha) {
            Boolean repetido = false;

            foreach (var item in this.misfichas)
            {
                if (item.color==ficha.color) {
                    repetido = true;
                    break;
                }

            }
            return repetido;
            
        }
        public void agregarFicha(Ficha ficha) {
            this.misfichas.Add(ficha);
        }
        
        public int id { set; get; }
        public string nombre { set; get; }

    }
}