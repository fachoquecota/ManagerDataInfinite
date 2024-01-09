using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ClienteController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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

            List<ClienteModel> modelos = new List<ClienteModel>();
            var httpClient = _httpClientFactory.CreateClient();

            var baseUrl = _configuration["OriginPathApi"];


            //using (var response = await httpClient.GetAsync("http://localhost:5172/api/Clientes/GetClientes"))
            using (var response = await httpClient.GetAsync(baseUrl + "api/Clientes/GetClientes"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseCliente>(apiResponse);
                modelos = responseObject.result;
            }

            //HttpResponseMessage tipoDocumentoResponse = await httpClient.GetAsync("http://localhost:5172/api/Clientes/GetTipoDocumento_ComboBox");
            HttpResponseMessage tipoDocumentoResponse = await httpClient.GetAsync(baseUrl + "api/Clientes/GetTipoDocumento_ComboBox");
            if (tipoDocumentoResponse.IsSuccessStatusCode)
            {
                var tipoDocumentoContent = await tipoDocumentoResponse.Content.ReadAsStringAsync();
                var tipoDocumentoData = JsonConvert.DeserializeObject<Dictionary<string, List<TipoDocumento>>>(tipoDocumentoContent);
                var tipoDocumento = tipoDocumentoData["result"];

                ViewBag.TipoDocumentos = tipoDocumento.Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.descripcion
                }).ToList();
            }
            return View(modelos);
        }
        public async Task<ClienteModel> GetClienteByID(int id)
        {
            ClienteModel cliente = new ClienteModel();
            var baseUrl = _configuration["OriginPathApi"];
            using (var httpClient = new HttpClient())
            {
                //using (var response = await httpClient.GetAsync($"http://localhost:5172/api/Clientes/GetClientesByid?idCliente={id}"))
                using (var response = await httpClient.GetAsync(baseUrl + $"api/Clientes/GetClientesByid?idCliente={id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cliente = JsonConvert.DeserializeObject<ClienteModel>(apiResponse);
                    }
                    else
                    {
                        // Manejo de error, dependiendo de tus necesidades
                        // Por ejemplo, puedes lanzar una excepción o devolver un valor nulo
                    }
                }
            }
            return cliente;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            //var response = await httpClient.PutAsJsonAsync("http://localhost:5172/api/Clientes/PutClientes", cliente);
            var response = await httpClient.PutAsJsonAsync(baseUrl + "api/Clientes/PutClientes", cliente);

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta exitosa
                return Ok();
            }
            else
            {
                // Manejar el error
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertCliente([FromBody] Cliente cliente)
        {
            var baseUrl = _configuration["OriginPathApi"];

            var httpClient = _httpClientFactory.CreateClient();
            //var response = await httpClient.PostAsJsonAsync("http://localhost:5172/api/Clientes/PostClientes", cliente);
            var response = await httpClient.PostAsJsonAsync(baseUrl + "api/Clientes/PostClientes", cliente);

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta (que asumimos que es el ID del cliente)
                var newClienteId = await response.Content.ReadAsStringAsync();
                return Ok(newClienteId);
            }
            else
            {
                // Manejar el error
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCliente(int idCliente)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            //var response = await httpClient.DeleteAsync($"http://localhost:5172/api/Clientes/DeleteClientes?idCliente={idCliente}");
            var response = await httpClient.DeleteAsync(baseUrl + $"api/Clientes/DeleteClientes?idCliente={idCliente}");

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta exitosa
                return Ok();
            }
            else
            {
                // Manejar el error
                return BadRequest();
            }
        }

        public class Cliente
        {
            public int idCliente { get; set; }
            public string nombreContacto { get; set; }
            public string apellidoContacto { get; set; }
            public string telefono { get; set; }
            public int idTipoDocumento { get; set; }
            public string numeroDocumento { get; set; }
            public string razonSocial { get; set; }
            public string nombreComercial { get; set; }
            public string correo { get; set; }
            public string direccion { get; set; }
        }
        public class TipoDocumento
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
    }
}
