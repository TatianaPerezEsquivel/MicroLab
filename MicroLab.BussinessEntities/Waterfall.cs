using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessEntities
{
    public class Waterfall
    {
        public int Id { get; set; }
 
        public string Name { get; set; } = string.Empty;
      

        [NotMapped]
        public int Top_Aux { get; set; }//propiedad auxiliar
    }
}
