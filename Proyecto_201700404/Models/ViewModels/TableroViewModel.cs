using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Models.ViewModels
{
    public class TableroViewModel
    {
        [Required]
        public int Fila { set; get; }

        [Required]
        public int Columna { set; get; }
    }
}