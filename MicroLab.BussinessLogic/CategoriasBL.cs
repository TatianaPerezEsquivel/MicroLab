using MicroLab.BussinessEntities;
using MicroLab.DataAccessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MicroLab.BussinessLogic.WaterfallBL;

namespace MicroLab.BussinessLogic
{
    public class CategoriasBL
    {
        private readonly CategoriasDAL categoriaDAL;

        public CategoriasBL()
        {
            categoriaDAL = new CategoriasDAL();
        }

        public async Task<List<Categorias>> ObtenerCategorias()
        {
            return await categoriaDAL.ObtenerCategorias();
        }
    
    }
}
