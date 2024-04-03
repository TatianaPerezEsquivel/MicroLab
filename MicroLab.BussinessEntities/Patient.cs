using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessEntities
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del paciente es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 de caracteres")]
        [Display(Name = "Nombre")]
  
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido del paciente es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 de caracteres")]
        [Display(Name = "Apellido")]
        public string lastname { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateOnly birthDate { get; set; }

        [Required(ErrorMessage = "La Edad es Rquerido")]
        [StringLength(3, ErrorMessage = "Maximo 3 de caracteres")]
        [Display(Name = "Edad")]
        public string Age { get; set; } = string.Empty;

        [Required(ErrorMessage = "El telefono del paciente es requerido")]
        [Display(Name = "Numero de Telefono")]
        public string? CellPhone { get; set; }

        [Required(ErrorMessage = "La dirección del paciente es requerida")]
        [StringLength(50, ErrorMessage = "Maximo 200 de caracteres")]
        [Display(Name = "Direccion")]
        public string address { get; set; } = string.Empty;

        [Required(ErrorMessage = "El género del paciente es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 20 de caracteres")]
        [Display(Name = "Genero")]
        public string gender { get; set; } = string.Empty;
        //public List<Exam> Exams { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }//propiedad auxiliar
    }
}
