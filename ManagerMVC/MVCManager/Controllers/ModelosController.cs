using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using static MVCManager.Controllers.VentaController;
using MVCManager.Models;

namespace MVCManager.Controllers
{
    public class ModelosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ModelosController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();

            List<ModeloProducto> modelos = new List<ModeloProducto>();

            //using (var response = await httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Productos/GETModeloProductosListaCrud"))
            using (var response = await httpClient.GetAsync(baseUrl + "api/Productos/GETModeloProductosListaCrud"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseModelo>(apiResponse);
                modelos = responseObject.result;
            }


            HttpResponseMessage categoriaResponse = await httpClient.GetAsync(baseUrl+"api/Productos/GetCrudCategoriaCrudCB");
            if (categoriaResponse.IsSuccessStatusCode)
            {
                var categoriaContent = await categoriaResponse.Content.ReadAsStringAsync();
                var categoriaData = JsonConvert.DeserializeObject<CategoriaResponse>(categoriaContent);

                ViewBag.Categoria = categoriaData.result.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Descripcion
                }).ToList();
            }
            return View(modelos);
        }
        public async Task<ModeloProducto> GetModeloProductoByID(int id)
        {
            ModeloProducto modelo = new ModeloProducto();
            var baseUrl = _configuration["OriginPathApi"];

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                //using (var response = await httpClient.GetAsync($"http://apiprosalesmanager.somee.com/api/Productos/GETModeloProductosByIDCrud?idProducto={id}"))
                using (var response = await httpClient.GetAsync(baseUrl + $"api/Productos/GETModeloProductosByIDCrud?idProducto={id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ApiResponseModelo>(apiResponse);
                    modelo = responseObject.result.FirstOrDefault();
                }
            }
            return modelo;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateModeloProducto([FromBody] ModeloProductoUpdate modeloProducto)

        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var model = new
                {
                    idModeloProductoDetalle = 0,
                    idModeloProducto = modeloProducto.idModeloProducto,
                    desModelo = modeloProducto.desModelo,
                    idCategoria = modeloProducto.idCategoria,
                    desCategoria = string.Empty

                };
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var baseUrl = _configuration["OriginPathApi"];

                //var response = await httpClient.PutAsync("http://localhost:5172/api/Productos/PUTUpdateModeloProducto", content);
                var response = await httpClient.PutAsync(baseUrl + "api/Productos/PUTUpdateModeloProducto", content);

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
        [HttpPost]
        public async Task<IActionResult> InsertModeloProducto([FromBody] ModeloProductoInsert modeloProducto)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                modeloProducto.desCategoria = string.Empty;
                var content = new StringContent(JsonConvert.SerializeObject(modeloProducto), Encoding.UTF8, "application/json");
                var baseUrl = _configuration["OriginPathApi"];

                //var response = await httpClient.PostAsync("http://localhost:5172/api/Productos/POSTInsertModeloProducto", content);
                var response = await httpClient.PostAsync(baseUrl + "api/Productos/POSTInsertModeloProducto", content);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteModeloProducto(int idModeloProducto)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var baseUrl = _configuration["OriginPathApi"];
                //var response = await httpClient.DeleteAsync($"http://apiprosalesmanager.somee.com/api/Productos/DELETEDeleteModeloProducto?idModeloProducto={idModeloProducto}");
                var response = await httpClient.DeleteAsync(baseUrl + $"api/Productos/DELETEDeleteModeloProducto?idModeloProducto={idModeloProducto}");

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
    }
}
public class ModeloProductoUpdate
{
    public int idModeloProductoDetalle { get; set; }
    public int idModeloProducto { get; set; }
    public string desModelo { get; set; }
    public int idCategoria { get; set; }
    public string desCategoria { get; set; }
}

public class ApiResponseModelo
{
    public List<ModeloProducto> result { get; set; }
}

public class ModeloProducto
{
    public int idModeloProductoDetalle { get; set; }
    public int idModeloProducto { get; set; }
    public string desModelo { get; set; }
    public int idCategoria { get; set; }
    public string desCategoria { get; set; }
}
public class ModeloProductoInsert
{
    public int idModeloProductoDetalle { get; set; }
    public int idModeloProducto { get; set; }
    public string desModelo { get; set; }
    public int idCategoria { get; set; }
    public string desCategoria { get; set; }
}
