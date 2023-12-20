using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MVCManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var ventasGrafico = await ObtenerDatosDeApiAsync<ApiResponseReporteVentaGrafico>("http://localhost:5172/api/Ventas/ReporteVentaGrafico");
            var ventasDetalleGrafico = await ObtenerDatosDeApiAsync<ApiResponseReporteVentaDetalleGrafico>("http://localhost:5172/api/Ventas/ReporteVentaDetalleGrafico");

            var viewModel = new DashboardViewModel
            {
                VentasGrafico = ventasGrafico.Result,
                VentasDetalleGrafico = ventasDetalleGrafico.Result
            };

            return View(viewModel);
        }

        private async Task<T> ObtenerDatosDeApiAsync<T>(string url) where T : new()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            return new T();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ApiResponseReporteVentaGrafico
    {
        public List<ReporteVentaGraficoModel> Result { get; set; }
    }
    public class ApiResponseReporteVentaDetalleGrafico
    {
        public List<ReporteVentaDetalleGraficoModel> Result { get; set; }
    }


}