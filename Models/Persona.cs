using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace CrudNetCore5.Models
{
    /// <summary>
    /// Entidad mapeada con la base de datos, generada con code first
    ///  con la libreria de EntityFramework.SqlServer y migrada con EntityFramework.Tools
    /// </summary>
    public class Persona
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo de Nombre es Obligatorio")]
        [StringLength(50,ErrorMessage = "El campo de Nombre tiene limite de 50 caracteres ")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo de Apellido es Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo de Apellido tiene limite de 50 caracteres ")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo de Numero de Documento es Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo de Numero de Documento tiene limite de 50 caracteres ")]
        [Display(Name = "Número de Documento")]
        public string NroDocumento { get; set; }
        [Required(ErrorMessage = "El campo de Correo Electronico es Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo de Correo Electronico tiene limite de 50 caracteres ")]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El campo de Telefono es Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo de Telefono tiene limite de 50 caracteres ")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo de Fecha de Nacimiento es Obligatorio")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
