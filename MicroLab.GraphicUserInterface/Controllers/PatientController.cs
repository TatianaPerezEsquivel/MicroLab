using MicroLab.BussinessEntities;
using MicroLab.BussinessLogic;
using MicroLab.DataAccessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MicroLab.GraphicUserInterface.Controllers
{

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PatientController : Controller
    {
        string ContextDB = "Data source = localhost; Initial catalog = Laboratorio; Integrated Security = true; Encrypt = false; TrustServerCertificate = true"; // Reemplaza con tu cadena de conex
        PatientBL patientBL = new PatientBL();
        // accion que muestra el listado de categorias
        // GET: PatientController
        public async Task<ActionResult> Index(Patient patient = null)
        {
            if (patient == null)
                patient = new Patient();
            if (patient.Top_Aux == 0)
                patient.Top_Aux = 10;// numero a mostrar por defecto
            else if (patient.Top_Aux == 1)
                patient.Top_Aux = 0;
            var patients = await patientBL.SearchAsync(patient);
            ViewBag.Top = patient.Top_Aux;

            return View(patients);
        }
        public ActionResult Obtener()
        {
            List<Categorias> categorias = new List<Categorias>();

            // Llamar al primer procedimiento almacenado para obtener las categorías
            using (SqlConnection connection = new SqlConnection(ContextDB))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("ObtenerCategorias", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Categorias categoria = new Categorias
                    {
                        IdCategoria = reader.GetInt32(0),
                        NombreCategoria = reader.GetString(1)
                    };
                    categorias.Add(categoria);
                }
                reader.Close();
            }

            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "NombreCategoria");

            return View();
                }
        //accion que  muestra el detalle de un registro
        public async Task<ActionResult> Details(int id)
        {
            var patient = await patientBL.GetByIdAsync(new Patient { Id = id });

            return View(patient);
        }

        // Accion que muestra el formulario para crear una nueva categoria 
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();

        }

        // Accion que recibe los datos del formulario y los evia a los bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Patient patient)
        {

            try
            {
                int result = await patientBL.CreateAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // accion que  muestra que muestra el formulario con datos cargados para modificar
        public  async Task<ActionResult> Edit(int id)
        {
            var patient = await patientBL.GetByIdAsync(new Patient { Id = id });
            ViewBag.Error = "";
            return View(patient);
        }


        // accion que recibe los datos modificados y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Patient patient)
        {
            try
            {
                int result = await patientBL.UpdateAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(patient);
            }
        }
        // accion que muestra los datos para confirmar la eliminacion
        public async Task<ActionResult> Delete(int id)
        {
            var patient = await patientBL.GetByIdAsync(new Patient { Id = id });
            ViewBag.Error = "";
            return View(patient);
        }

        // accion que recibe la confirmacion para eliminar el registro

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Patient patient)
        {
            try
            {
                int result = await patientBL.DeleteAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {


                ViewBag.Error = ex.Message;

                return View(patient);
            }
        }
       
        }
    }

