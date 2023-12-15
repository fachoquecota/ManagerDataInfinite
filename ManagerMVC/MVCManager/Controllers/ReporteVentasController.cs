using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using ClosedXML.Excel;

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
                var ubigeoes = ubigeoData.Result;
                var departamentos = ubigeoes.Select(u => u.Departamento).Distinct().ToList();


                ViewBag.Departamentos = departamentos;


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

        [HttpPost]
        public async Task<IActionResult> GetVentasFiltro([FromBody] FiltroVentasModel filtro)
        {
            var content = JsonConvert.SerializeObject(filtro);
            var httpResponse = await _httpClient.PostAsync("http://localhost:5172/api/Ventas/GetVentasFiltro", new StringContent(content, System.Text.Encoding.UTF8, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            List<ReporteventasModel> modelos;

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ApiResponseVentas>(jsonResponse);
            modelos = responseObject.result;
            return Ok(modelos);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetalleVenta(int idVenta)
        {
            var httpResponse = await _httpClient.GetAsync($"http://localhost:5172/api/Ventas/GetReporteVentaDetalle?idVenta={idVenta}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ApiResponseDetalleVentas>(jsonResponse);

            return Ok(responseObject.result);
        }

        public async Task<IActionResult> DescargarReporteVentas([FromBody] FiltroVentasModel filtro)
        {
            var content1 = JsonConvert.SerializeObject(filtro);
            var httpResponse = await _httpClient.PostAsync("http://localhost:5172/api/Ventas/GetVentasFiltro", new StringContent(content1, Encoding.UTF8, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<ApiResponseVentas>(jsonResponse);
            var modelos = responseObject.result;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte de Ventas");
                var currentRow = 1;

                // Encabezados
                worksheet.Cell(currentRow, 1).Value = "ID Venta";
                // Añadir otros encabezados...

                // Datos
                foreach (var modelo in modelos)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = modelo.idVenta;
                    // Añadir otros datos...
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVentas.xlsx");
                }
            }
        }


    }

    public class TransporteCB
    {
        public int id { get; set; }
        public string descripcion { get; set; }
    }
    public class VentaFiltroResultado
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public int IdTipoPago { get; set; }
        public string TipoPago { get; set; }
        public int IdEmpresaTranspte { get; set; }
        public decimal Total { get; set; }
        public string FechaVenta { get; set; }
        public string EmpresaTransporte { get; set; }
        public int IdUbigeo { get; set; }
        public string Ubigeo { get; set; }
    }
    public class FiltroVentasModel
    {
        public int idCliente { get; set; }
        public int idEmpresaTranspte { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public DateTime? fechaInicio { get; set; }
        public DateTime? fechaFin { get; set; }
    }


    public class ApiResponseDetalleVentas
    {
        public List<ReporteVentasDetalleModel> result { get; set; }
    }

    public class ReporteVentasDetalleModel
    {
        public int idVenta { get; set; }
        public string fechaVenta { get; set; }
        public string totalDefinido { get; set; }
        public string imagenCarpeta { get; set; }
        public string imagenNombre { get; set; }
        public string producto { get; set; }
        public string calidad { get; set; }
        public string cantidad { get; set; }
        public string talla { get; set; }
        public string precioUnitario { get; set; }
        public string subtotal { get; set; }
    }
}
