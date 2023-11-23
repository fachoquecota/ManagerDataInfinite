using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<ActionResult> Index()
        {
            List<ClienteModel> modelos = new List<ClienteModel>();
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync("http://localhost:5172/api/Clientes/GetClientes"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseCliente>(apiResponse);
                modelos = responseObject.result;
            }



            HttpResponseMessage tipoDocumentoResponse = await httpClient.GetAsync("http://localhost:5172/api/Clientes/GetTipoDocumento_ComboBox");
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
        public async Task<ModeloProducto> GetClienteByID(int id)
        {
            ModeloProducto modelo = new ModeloProducto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5172/api/Clientes/GetClientesByid?idCliente{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    modelo = responseObject.result.FirstOrDefault();
                }
            }
            return modelo;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            var httpClient = _httpClientFactory.CreateClient();
            cliente.desTipoDocumento = "";
            var response = await httpClient.PutAsJsonAsync("http://localhost:5172/api/Clientes/PutClientes", cliente);

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
            var httpClient = _httpClientFactory.CreateClient();
            cliente.desTipoDocumento = "";
            var response = await httpClient.PutAsJsonAsync("http://localhost:5172/api/Clientes/PutClientes", cliente);

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
            public string desTipoDocumento { get; set; }
        }



        public class TipoDocumento
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
    }
}
