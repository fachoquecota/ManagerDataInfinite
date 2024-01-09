using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MVCManager.Controllers
{
    public class CalidadController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CalidadController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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

            List<CalidadModel> modelos = new List<CalidadModel>();
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync(_configuration["OriginPathApi"] + "api/Calidad/GetAllCalidad"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseCalidad>(apiResponse);
                modelos = responseObject.calidad;
            }

            return View(modelos);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCalidad([FromQuery] string descripcion)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];
            var resourceUrl = $"api/Calidad/PostCalidad?descripcion={Uri.EscapeDataString(descripcion)}";
            var fullUrl = baseUrl + resourceUrl;

            var response = await httpClient.PostAsync(fullUrl, null);

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
        public async Task<IActionResult> UpdateCalidad([FromQuery] int idCalidad, [FromQuery] string descripcion)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];
            var resourceUrl = $"api/Calidad/PutCalidad?idCalidad={idCalidad}&descripcion={Uri.EscapeDataString(descripcion)}";
            var url = baseUrl + resourceUrl;
            //var url = $"http://localhost:5172/api/Calidad/PutCalidad?idCalidad={idCalidad}&descripcion={Uri.EscapeDataString(descripcion)}";

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
        public async Task<IActionResult> DeleteCalidad(int idCalidad)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var baseUrl = _configuration["OriginPathApi"];
            var deleteUrl = $"api/Calidad/DeleteCalidad?idCalidad={idCalidad}";
            var url = baseUrl + deleteUrl;

            //var url = $"http://apiprosalesmanager.somee.com/api/Calidad/DeleteCalidad?idCalidad={idCalidad}";
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
        public async Task<ModeloProducto> GetModeloProductoByID(int id)
        {
            ModeloProducto modelo = new ModeloProducto();
            using (var httpClient = new HttpClient())
            {
                var baseUrl = _configuration["OriginPathApi"];
                var getUrl = $"api/Productos/GETModeloProductosByIDCrud?idProducto={id}";
                var fullGetUrl = baseUrl + getUrl;

                //using (var response = await httpClient.GetAsync($"http://apiprosalesmanager.somee.com/api/Productos/GETModeloProductosByIDCrud?idProducto={id}"))
                using (var response = await httpClient.GetAsync(fullGetUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        modelo = JsonConvert.DeserializeObject<ModeloProducto>(apiResponse);
                    }
                    else
                    {
                        // Manejo de error, dependiendo de tus necesidades
                        // Por ejemplo, puedes lanzar una excepción o devolver un valor nulo
                    }
                }
            }
            return modelo;
        }

    }

    public class ApiResponseCalidad
    {
        public List<CalidadModel> calidad { get; set; }
    }
}
