using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Models.ViewModels
{
    public class ArchivoViewModel
    {
        [Required]
        [DisplayName("Mi archivo")]
        public HttpPostedFileBase CargarArchivo { get; set; }
        [Required]
        [DisplayName("Ni cadena")]
        public string cadena { get; set; }
    }
}