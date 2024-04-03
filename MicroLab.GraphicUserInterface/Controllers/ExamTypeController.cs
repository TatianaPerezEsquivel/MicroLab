using MicroLab.BussinessEntities;
using MicroLab.BussinessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroLab.GraphicUserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ExamTypeController : Controller
    {
    
        ExamTypeBL examTypeBL = new ExamTypeBL();
        // accion que muestra el listado de Tipo de examenes
        public async Task<IActionResult> Index(ExamType examType = null)
        {
            if (examType == null)
                examType = new ExamType();
            if (examType.Top_Aux == 0)
                examType.Top_Aux = 10;// numero a mostrar por defecto
            else if (examType.Top_Aux == 1)
                examType.Top_Aux = 0;
            var examTypes = await examTypeBL.SearchAsync(examType);
            ViewBag.Top = examType.Top_Aux;



            return View(examTypes);
        }

        //accion que  muestra el detalle de un registro
        public async Task<IActionResult> Details(int id)
        {
            var examType = await examTypeBL.GetByIdAsync(new ExamType { Id = id });

            return View(examType);
        }

        // Accion que muestra el formulario para crear un nuevo Tipo de examen
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Accion que recibe los datos del formulario y los evia a los bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExamType examType)
        {
            try
            {
                int result = await examTypeBL.CreateAsync(examType);
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
            var examType = await examTypeBL.GetByIdAsync(new ExamType { Id = id });
            ViewBag.Error = "";
            return View(examType);
        }

        // accion que recibe los datos modificados y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExamType examType)
        {
            try
            {
                int result = await examTypeBL.UpdateAsync(examType);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(examType);
            }
        }

        // accion que muestra los datos para confirmar la eliminacion
        public async Task<IActionResult> Delete(int id)
        {
            var examType = await examTypeBL.GetByIdAsync(new ExamType { Id = id });
            ViewBag.Error = "";
            return View(examType);
        }

        // accion que recibe la confirmacion para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ExamType examType)
        {
            try
            {
                int result = await examTypeBL.DeleteAsync(examType);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {


                ViewBag.Error = ex.Message;

                return View(examType);
            }
        }
    }
}
