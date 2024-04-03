using MicroLab.BussinessEntities;
using MicroLab.DataAccessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessLogic
{
    public class WaterfallBL
    {

        public class CategoriaBLL
        {
            private readonly ContextDB _context;

            public CategoriaBLL(ContextDB context)
            {
                _context = context;
            }

            public List<Waterfall> ObtenerCategorias()
            {
                return _context.Waterfall.ToList();
            }
        }

    }
}


