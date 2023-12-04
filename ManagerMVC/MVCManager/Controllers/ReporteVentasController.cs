using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Globalization;
using static MVCManager.Controllers.VentaController;

namespace MVCManager.Controllers
{
    public class ReporteVentasController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<ActionResult> Index()
        {

            List<ReporteventasModel> modelos;

            using (var response = await _httpClient.GetAsync("http://localhost:5172/api/Ventas/GetVentas"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<ApiResponseVentas>(apiResponse);
                modelos = responseObject.result;

                foreach (var modelo in modelos)
                {
                    // Convierte la fecha de string a DateTime y luego a string con el formato deseado
                    if (DateTime.TryParse(modelo.fechaVenta, out DateTime fechaParsed))
                    {
                        modelo.fechaVenta = fechaParsed.ToString("dd/MM/yyyy");
                    }
                }
            }


            HttpResponseMessage transporteResponse = await _httpClient.GetAsync("http://localhost:5172/api/EmpresaTransporte/GetAllTransporteCombobox");
            if (transporteResponse.IsSuccessStatusCode)
            {
                var transporteContent = await transporteResponse.Content.ReadAsStringAsync();
                var transporteData = JsonConvert.DeserializeObject<Dictionary<string, List<TransporteCB>>>(transporteContent);
                var transporte = transporteData["result"];

                ViewBag.Transporte = transporte.Select(c => new SelectListItem
                {
                    Value = c.id.ToString(),
                    Text = c.descripcion
                }).ToList();
            }

           
            return View(modelos);
        }

    }

    public class TransporteCB
    {
        public int id { get; set; }
        public string descripcion { get; set; }
    }
}
