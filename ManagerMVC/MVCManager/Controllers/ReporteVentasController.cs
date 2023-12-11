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
            HttpResponseMessage ubigeoResponse = await _httpClient.GetAsync("http://localhost:5172/api/Ventas/GetUbigeo");
            if (ubigeoResponse.IsSuccessStatusCode)
            {
                var ubigeoContent = await ubigeoResponse.Content.ReadAsStringAsync();
                var ubigeoData = JsonConvert.DeserializeObject<ApiResponseUbigeo>(ubigeoContent);

                // Aquí tienes la lista completa de Ubigeo
                var ubigeoes = ubigeoData.Result;

                // Puedes preparar los datos para los comboboxes aquí si es necesario
                // Por ejemplo, obtener la lista de departamentos únicos
                var departamentos = ubigeoes.Select(u => u.Departamento).Distinct().ToList();
                ViewBag.Departamentos = departamentos;

                // Pasar la lista completa a la vista también podría ser útil
                ViewBag.Ubigeo = ubigeoes;
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

            HttpResponseMessage clienteResponse = await _httpClient.GetAsync("http://localhost:5172/api/Clientes/GetClientes");
            if (clienteResponse.IsSuccessStatusCode)
            {
                var clienteContent = await clienteResponse.Content.ReadAsStringAsync();
                var clienteData = JsonConvert.DeserializeObject<ApiResponseCliente>(clienteContent);

                ViewBag.Cliente = clienteData.result.Select(c => new SelectListItem
                {
                    Value = c.idCliente.ToString(),
                    Text = c.nombreContacto + " " + c.apellidoContacto + " - " + c.numeroDocumento,
                }).ToList();
            }

            HttpResponseMessage transResponse = await _httpClient.GetAsync("http://localhost:5172/api/EmpresaTransporte/GetAllTransporteCombobox");
            if (transResponse.IsSuccessStatusCode)
            {
                var transContent = await transResponse.Content.ReadAsStringAsync();
                var transData = JsonConvert.DeserializeObject<Dictionary<string, List<TipoDocumento>>>(transContent);
                var trans = transData["result"];

                ViewBag.TransDocumentos = trans.Select(c => new SelectListItem
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
