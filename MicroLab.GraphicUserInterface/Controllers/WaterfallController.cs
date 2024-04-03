using MicroLab.BussinessEntities;
using MicroLab.BussinessLogic;
using MicroLab.GraphicUserInterface.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace MicroLab.GraphicUserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class WaterfallController : Controller
    {
        ExamTypeBL examTypeBL = new ExamTypeBL();

        private readonly string _cadenaSQL;

        public WaterfallController(IConfiguration config)
        {
            _cadenaSQL = config.GetConnectionString("cadenaSQL");
        }

        public async Task<IActionResult> Index()
        {
            var types = await examTypeBL.GetAllAsync();
            ViewBag.Types = types;
           return View();
        }
        public JsonResult ObtenerCategorias(string name)
        {

            List<Waterfall> lista = new List<Waterfall>();

            using (var cn = new SqlConnection(_cadenaSQL))
            {
                cn.Open();
                var cmd = new SqlCommand("SPObtenerWaterfall", cn);
                cmd.Parameters.AddWithValue("Name", name);
               
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Waterfall
                        {
                            Name = dr["Name"].ToString(),
                          
                        });
                    }
                }
            }

            return Json(lista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
