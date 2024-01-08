using Microsoft.AspNetCore.Mvc;

namespace MVCManager.Controllers
{
    public class ConfiguracionController : Controller
    {
        public IActionResult Index()
        {
            // Verificar si el usuario está autenticado (verificar la existencia del token)
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                // Si no hay token, redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
