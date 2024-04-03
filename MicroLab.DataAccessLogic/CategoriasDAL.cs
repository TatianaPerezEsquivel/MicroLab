using MicroLab.BussinessEntities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.DataAccessLogic
{
    public class CategoriasDAL
    {
        string ContextDB = "ContextDB"; // Reemplaza con tu cadena de conexión

        public async Task<List<Categorias>> ObtenerCategorias()
        {
            List<Categorias> categorias = new List<Categorias>();

            using (SqlConnection connection = new SqlConnection(ContextDB))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("ObtenerCategorias", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Categorias categoria = new Categorias
                            {
                                IdCategoria = reader.GetInt32(0),
                                NombreCategoria = reader.GetString(1)
                            };
                            categorias.Add(categoria);
                        }
                    }
                }
            }

            return categorias;
        }
    }
}
