using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MVCManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            // Verificar si el usuario está autenticado (verificar la existencia del token)
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                // Si no hay token, redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Index", "Login");
            }

            var ventasResponse = await GetVentas();
            var detallesVentasResponse = await GetDetalleVentas();

            var modeloMasVendido = detallesVentasResponse.Result
                .GroupBy(dv => dv.NombreModeloProducto)
                .OrderByDescending(gp => gp.Count())
                .Select(gp => gp.Key)
                .FirstOrDefault();

            var marcaMasVendida = detallesVentasResponse.Result
                .GroupBy(dv => dv.NombreMarca)
                .OrderByDescending(gp => gp.Count())
                .Select(gp => gp.Key)
                .FirstOrDefault();

            var añoActual = DateTime.Now.Year;
            var ventasPorMes = ventasResponse.Result
                .Where(v => v.FechaVenta.Year == añoActual)
                .GroupBy(v => v.FechaVenta.ToString("MMM"))
                .ToDictionary(g => g.Key, g => g.Sum(v => decimal.Parse(v.TotalDefinido)));

            var ventasPorCalidad = detallesVentasResponse.Result
                .GroupBy(dv => dv.NombreCalidad)
                .ToDictionary(g => g.Key, g => g.Count());

            var ventasPorModelo = detallesVentasResponse.Result
                .GroupBy(dv => dv.NombreModeloProducto)
                .ToDictionary(g => g.Key, g => g.Count());

            var viewModel = new DashboardViewModel
            {
                Ventas = ventasResponse.Result,
                DetalleVentas = detallesVentasResponse.Result,
                DepartamentoConMasVentas = ventasResponse.Result
                    .GroupBy(v => v.NombreDepartamento)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefault(),
                ModeloProductoMasVendido = modeloMasVendido,
                MarcaMasVendida = marcaMasVendida,
                VentasPorMes = ventasPorMes,
                VentasPorCalidad = ventasPorCalidad,
                VentasPorModelo = ventasPorModelo

            };

            return View(viewModel);
        }
        private async Task<VentaResponse> GetVentas()
        {
            var baseUrl = _configuration["OriginPathApi"];
            var client = _httpClientFactory.CreateClient();
            //var response = await client.GetAsync("http://localhost:5172/api/Ventas/ReporteVentaGrafico");
            var response = await client.GetAsync(baseUrl + "api/Ventas/ReporteVentaGrafico");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VentaResponse>(jsonResponse);
        }

        private async Task<DetalleVentaResponse> GetDetalleVentas()
        {
            var baseUrl = _configuration["OriginPathApi"];
            var client = _httpClientFactory.CreateClient();
            //var response = await client.GetAsync("http://localhost:5172/api/Ventas/ReporteVentaDetalleGrafico");
            var response = await client.GetAsync(baseUrl + "api/Ventas/ReporteVentaDetalleGrafico");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DetalleVentaResponse>(jsonResponse);
        }
    }
}