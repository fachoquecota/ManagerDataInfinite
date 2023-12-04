using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class TransporteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransporteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> Index()
        {
            List<TransporteModel> modelos = new List<TransporteModel>();
            var httpClient = _httpClientFactory.CreateClient();
            using (var response = await httpClient.GetAsync("http://localhost:5172/api/EmpresaTransporte/GetAllEmpresaTransporte"))
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
            var url = $"http://localhost:5172/api/EmpresaTransporte/PostTransporte?descripcion={Uri.EscapeDataString(descripcion)}&direccion={Uri.EscapeDataString(direccion)}";

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

            var url = $"http://localhost:5172/api/EmpresaTransporte/PutTransporte?idEmpresaTranspte={idEmpresaTransporte}&descripcion={Uri.EscapeDataString(descripcion)}&direccion={Uri.EscapeDataString(direccion)}";
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
            var url = $"http://localhost:5172/api/EmpresaTransporte/DeleteTransporte?idEmpresaTranspte={idEmpresaTransporte}";
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
