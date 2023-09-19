using Microsoft.AspNetCore.Mvc;

namespace MVCManager.Controllers
{
    public class ColorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
