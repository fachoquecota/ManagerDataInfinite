using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace MVCManager.Controllers
{
    public class ModelosController : Controller
    {
        public async Task<ActionResult> Index()
        {
            List<ModeloProducto> modelos = new List<ModeloProducto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Productos/GETModeloProductosListaCrud"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    modelos = responseObject.result;
                }
            }
            return View(modelos);
        }
        public async Task<ModeloProducto> GetModeloProductoByID(int id)
        {
            ModeloProducto modelo = new ModeloProducto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://apiprosalesmanager.somee.com/api/Productos/GETModeloProductosByIDCrud?idProducto={id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse>(apiResponse);
                    modelo = responseObject.result.FirstOrDefault();
                }
            }
            return modelo;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateModeloProducto([FromBody] ModeloProductoUpdate modeloProducto)

        {
            using (var httpClient = new HttpClient())
            {
                var model = new
                {
                    idModeloProducto = modeloProducto.idModeloProducto,
                    descripcion = modeloProducto.descripcion
                };
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync("http://apiprosalesmanager.somee.com/api/Productos/PUTUpdateModeloProducto", content);
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
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(modeloProducto), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("http://apiprosalesmanager.somee.com/api/Productos/POSTInsertModeloProducto", content);
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
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync($"http://apiprosalesmanager.somee.com/api/Productos/DELETEDeleteModeloProducto?idModeloProducto={idModeloProducto}");

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
    public int idModeloProducto { get; set; }
    public string descripcion { get; set; }
}

public class ApiResponse
{
    public List<ModeloProducto> result { get; set; }
}

public class ModeloProducto
{
    public int idModeloProducto { get; set; }
    public string descripcion { get; set; }
}
public class ModeloProductoInsert
{
    public int idModeloProducto { get; set; }
    public string descripcion { get; set; }
}
