using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCManager.Models;
using Newtonsoft.Json;

namespace MVCManager.Controllers
{
    public class ReporteVentasController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<ActionResult> Index()
        {

            // Primera llamada API
            List<ReporteventasModel> modelos = new List<ReporteventasModel>();
            using (var response = await _httpClient.GetAsync("http://localhost:5172/api/Ventas/GetVentas"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseVentas>(apiResponse);
                modelos = responseObject.result;
            }


            HttpResponseMessage categoriaResponse = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Productos/GetCrudCategoriaCrudCB");
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
            HttpResponseMessage generoResponse = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudGeneroCrudCB");
            if (generoResponse.IsSuccessStatusCode)
            {
                var generoContent = await generoResponse.Content.ReadAsStringAsync();
                var generoData = JsonConvert.DeserializeObject<CategoriaResponse>(generoContent);

                ViewBag.Genero = generoData.result.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Descripcion
                }).ToList();
            }
            return View(modelos);
        }

    }
}
