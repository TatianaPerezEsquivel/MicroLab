using MicroLab.BussinessEntities;
using MicroLab.DataAccessLogic;
using MicroLab.BussinessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroLab.GraphicUserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class ExamController : Controller
    {
        ExamBL examBL = new ExamBL();

    
        public async Task<IActionResult> GenerateReport()
        {
            ViewBag.Error = "";
            return View();
        }

        //prueba
        [HttpPost]
        public async Task<IActionResult> Report(DateTime startDate, DateTime endDate)
        {

            try
            {
                // Asegúrate de ajustar las fechas al rango completo del día
                endDate = endDate.AddDays(1).AddTicks(-1);

                var examsInRange = await examBL.GetExamsInRange(startDate, endDate);

                return View(examsInRange);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la generación del reporte
                ViewBag.Error = "Error al generar el reporte: " + ex.Message;
                return View();
            }
        }


        // accion que muestra el listado de examenes

        public async Task<IActionResult> Index(Exam exam = null)
        {
            if (exam == null)
                exam = new Exam();
            if (exam.Top_Aux == 0)
                exam.Top_Aux = 10;// numero a mostrar por defecto
            else if (exam.Top_Aux == 1)
                exam.Top_Aux = 0;
            var exames = await examBL.GetAllAsync();
            ViewBag.Top = exam.Top_Aux;



            return View(exames);
        }

        //accion que muestra el detalle del registro

        public async Task<IActionResult> Details(int id)
        {
            var exam = await examBL.GetByIdAsync(new Exam { Id = id });

            return View(exam);
        }
        // Accion que muestra el formulario para crear un nuevo examen

        public async  Task<IActionResult> Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Accion que recibe los datos del formulario y los evia a los bd

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exam exam)
        {
            try
            {
                exam.Date = DateTime.Now;
                int result = await examBL.CreateAsync(exam);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();

            }
        }
        // accion que  uestra que muestra el formulario con datos cargados para modificar

        public async Task<IActionResult> Edit(int id)
        {
            var exam = await examBL.GetByIdAsync(new Exam { Id = id });
            ViewBag.Error = "";
            return View(exam);
        }
        // accion que recibe los datos modificados y los envia a la bd

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(int id, Exam exam)
        {
            try
            {
                int result = await examBL.UpdateAsync(exam);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(exam);
            }
        }
        // accion que muestra los datos para confirmar la eliminacion

        public async  Task<IActionResult> Delete(int id)
        {
            var exam = await examBL.GetByIdAsync(new Exam { Id = id });
            ViewBag.Error = "";
            return View(exam);
        }

        // accion que muestra los datos para confirmar la eliminacion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Exam exam)
        {
            try
            {
                int result = await examBL.DeleteAsync(exam);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
