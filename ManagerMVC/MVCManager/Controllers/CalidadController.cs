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

        public CalidadController(IHttpClientFactory httpClientFactory)
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

            List<CalidadModel> modelos = new List<CalidadModel>();
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Calidad/GetAllCalidad"))
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
            var response = await httpClient.PostAsync($"http://apiprosalesmanager.somee.com/api/Calidad/PostCalidad?descripcion={Uri.EscapeDataString(descripcion)}", null);

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

            var url = $"http://localhost:5172/api/Calidad/PutCalidad?idCalidad={idCalidad}&descripcion={Uri.EscapeDataString(descripcion)}";
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
            var url = $"http://apiprosalesmanager.somee.com/api/Calidad/DeleteCalidad?idCalidad={idCalidad}";
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
                using (var response = await httpClient.GetAsync($"http://apiprosalesmanager.somee.com/api/Productos/GETModeloProductosByIDCrud?idProducto={id}"))
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
