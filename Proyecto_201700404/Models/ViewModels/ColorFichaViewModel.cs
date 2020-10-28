using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Proyecto_201700404.Models.ViewModels
{
    
    public class ColorFichaViewModel
    {

        //[Required]
        //public int idcolor1 { set; get; }
        //[Required]
        //public int idcolor2 { set; get; }

        [Required]
        public int idcolor { set; get; }
        public string color { set; get; }
        public string clase { set; get; }
    }
}