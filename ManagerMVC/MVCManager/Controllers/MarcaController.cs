using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class MarcaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MarcaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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

            List<MarcaModel> modelos = new List<MarcaModel>();
            var httpClient = _httpClientFactory.CreateClient();

            using (var response = await httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Marca/GetAllMarca"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseCalidad>(apiResponse);
                modelos = responseObject.calidad;
            }

            return View(modelos);
        }

        [HttpPost]
        public async Task<IActionResult> InsertMarca([FromQuery] string descripcion)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync($"http://apiprosalesmanager.somee.com/api/Marca/PostMarca?descripcion={Uri.EscapeDataString(descripcion)}", null);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                // Podrías querer leer el cuerpo de la respuesta para mostrar un mensaje más específico
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMarca([FromQuery] int idMarca, [FromQuery] string descripcion)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = $"http://localhost:5172/api/Marca/PutMarca?idMarca={idMarca}&descripcion={Uri.EscapeDataString(descripcion)}";
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
        public async Task<IActionResult> DeleteMarca([FromQuery] int idMarca) 
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = $"http://localhost:5172/api/Marca/DeleteMarca?idMarca={idMarca}";

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

        public class ApiResponseCalidad
        {
            public List<MarcaModel> calidad { get; set; }
        }
    }

}
