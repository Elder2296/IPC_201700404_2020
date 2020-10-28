using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_201700404.Models.ViewModels
{
    public class InvitadoViewModel
    {
        [Required]
        public string nombre { set; get; }
    }
}