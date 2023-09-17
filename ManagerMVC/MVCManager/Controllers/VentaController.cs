using Microsoft.AspNetCore.Mvc;

namespace MVCManager.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
