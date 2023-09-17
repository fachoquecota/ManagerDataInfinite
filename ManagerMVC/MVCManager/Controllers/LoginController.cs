using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;

namespace MVCManager.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Esto evita que se utilice el _Layout
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Aquí puedes implementar la lógica de autenticación
            // Por ejemplo, verifica si el usuario y la contraseña son correctos

            // Si la autenticación es exitosa, redirige a la página de inicio
            return RedirectToAction("Index", "Home");
        }


    }
}
