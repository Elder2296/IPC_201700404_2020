using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Clases
{
    public class Jugador
    {
        //public int identificador { set; get; }
        public string Alias { set; get; }

        List<Ficha> misfichas;
        public Jugador() { 
            misfichas= new List<Ficha>();

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