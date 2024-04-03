using MicroLab.BussinessEntities;
using MicroLab.BussinessLogic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdsProject.GraphicUserInterface.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Administrador")]
    public class RoleController : Controller
    {
        RoleBL roleBL = new RoleBL();
        // accion que muestra el listado de categorias
        public async Task<ActionResult> Index(Role role = null)
        {
            if (role == null)
                role = new Role();
            if (role.Top_Aux == 0)
                role.Top_Aux = 10;// numero a mostrar por defecto
            else if (role.Top_Aux == 1)
                role.Top_Aux = 0;
            var roles = await roleBL.SearchAsync(role);
            ViewBag.Top = role.Top_Aux;



            return View(roles);
        }

        //accion que  muestra el detalle de un registro
        public async Task<ActionResult> Details(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });

            return View(role);
        }

        // Accion que muestra el formulario para crear un rol 
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // Accion que recibe los datos del formulario y los evia a los bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Role role)
        {
            try
            {
                int result = await roleBL.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }

        // accion que  uestra que muestra el formulario con datos cargados para modificar
        public async Task<ActionResult> Edit(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // accion que recibe los datos modificados y los envia a la bd
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Role role)
        {
            try
            {
                int result = await roleBL.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(role);
            }
        }

        // accion que muestra los datos para confirmar la eliminacion
        public async Task<ActionResult> Delete(int id)
        {
            var role = await roleBL.GetByIdAsync(new Role { Id = id });
            ViewBag.Error = "";
            return View(role);
        }

        // accion que recibe la confirmacion para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Role role)
        {
            try
            {
                int result = await roleBL.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {


                ViewBag.Error = ex.Message;

                return View(role);
            }
        }
    }
}
