using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
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
    }
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