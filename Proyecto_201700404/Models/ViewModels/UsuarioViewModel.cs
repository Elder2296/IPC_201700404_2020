using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Proyecto_201700404.Models.ViewModels
{
    public class UsuarioViewModel
    {
        
        public int id_usuario { get; set; }
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Required]
        [StringLength(50)]

        public string apellido { get; set; }
        [Required]
        [StringLength(30)]
        public string nombreUsuario { get; set; }
        [Required]
        [StringLength(25)]

        public string tipoUsuario { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string contrasenia { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaNac { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public List<CIUDAD> ciudades { get; set; }
        public int id_ciudad { get; set; }  
    }
}