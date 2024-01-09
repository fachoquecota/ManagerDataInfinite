using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CategoriaController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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

            List<CategoriaModel> categorias = new List<CategoriaModel>();
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            //using (var response = await httpClient.GetAsync("http://localhost:5172/api/Categoria/GetAllCategoria"))
            using (var response = await httpClient.GetAsync(baseUrl + "api/Categoria/GetAllCategoria"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseCategoria>(apiResponse);
                categorias = responseObject.result;

            }

            return View(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategoria([FromQuery] string descripcion)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            var url = baseUrl + $"api/Categoria/PostCategoria?descripcion={Uri.EscapeDataString(descripcion)}";

            //var url = $"http://localhost:5172/api/Categoria/PostCategoria?descripcion={Uri.EscapeDataString(descripcion)}";

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
        public async Task<IActionResult> UpdateCategoria([FromQuery] int idCategoria, [FromQuery] string descripcion)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            var url = baseUrl + $"api/Categoria/PutCategoria?idCategoria={idCategoria}&descripcion={Uri.EscapeDataString(descripcion)}";
            //var url = $"http://localhost:5172/api/Categoria/PutCategoria?idCategoria={idCategoria}&descripcion={Uri.EscapeDataString(descripcion)}";
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
        public async Task<IActionResult> DeleteCategoria(int idCategoria)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            var url = baseUrl + $"api/Categoria/DeleteCategoria?idCategoria={idCategoria}";
            //var url = $"http://localhost:5172/api/Categoria/DeleteCategoria?idCategoria={idCategoria}";
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
    public class ApiResponseCategoria
    {
        public List<CategoriaModel> result { get; set; }
    }
}
