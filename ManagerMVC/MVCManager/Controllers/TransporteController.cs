using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class TransporteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;


        public TransporteController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ActionResult> Index()
        {
            // Verificar si el usuario está autenticado (verificar la existencia del token)
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                // Si no hay token, redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Index", "Login");
            }

            List<TransporteModel> modelos = new List<TransporteModel>();
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];

            using (var response = await httpClient.GetAsync(baseUrl+"api/EmpresaTransporte/GetAllEmpresaTransporte"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                modelos = responseObject.result;
            }

            return View(modelos);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTransporte([FromQuery] string descripcion, [FromQuery] string direccion)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];

            var url = $"{baseUrl}api/EmpresaTransporte/PostTransporte?descripcion={Uri.EscapeDataString(descripcion)}&direccion={Uri.EscapeDataString(direccion)}";

            var response = await httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTransporte([FromQuery] int idEmpresaTransporte, [FromQuery] string descripcion, [FromQuery] string direccion)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];

            var url = $"{baseUrl}api/EmpresaTransporte/PutTransporte?idEmpresaTranspte={idEmpresaTransporte}&descripcion={Uri.EscapeDataString(descripcion)}&direccion={Uri.EscapeDataString(direccion)}";
            var response = await httpClient.PutAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteTransporte(int idEmpresaTransporte)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];

            var url = $"{baseUrl}api/EmpresaTransporte/DeleteTransporte?idEmpresaTranspte={idEmpresaTransporte}";
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


    }
    public class ApiResponse
    {
        public List<TransporteModel> result { get; set; }
    }
}
