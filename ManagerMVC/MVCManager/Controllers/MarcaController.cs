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
        public class ApiResponseCalidad
        {
            public List<MarcaModel> calidad { get; set; }
        }
    }

}
