using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessEntities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del tipo es requerido")]
        [StringLength(50, ErrorMessage = "Maximo 50 de caracteres")]
        [Display(Name = "Nombre")]


        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public DateTime Date { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }//propiedad auxiliar


    }
}
