using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;

namespace MVCManager.Controllers
{
    public class VentaController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<IActionResult> Index(int pagina = 1)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudProductos");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productosResponse = JsonConvert.DeserializeObject<ProductosResponseModel>(content);

                // Filtramos los productos activos primero
                var productosActivos = productosResponse.Products.Where(p => p.Activo).ToList();

                var productos = productosActivos.OrderByDescending(p => p.IdProducto).ToList();

                int registrosPorPagina = 10;
                var productosPaginados = productos.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
                ViewBag.PaginaActual = pagina;
                ViewBag.TotalPaginas = Math.Ceiling((double)productos.Count / registrosPorPagina);

                return View(productosPaginados);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult GuardarVenta([FromBody] VentaData ventaData)
        {
            foreach (var producto in ventaData.productos)
            {
                // Aquí puedes procesar cada producto
                var id = producto.id;
                var cantidad = producto.cantidad;
                var precio = producto.precio;
                var total = producto.total;

                // Haz lo que necesites con estos datos
            }

            // Aquí puedes procesar el total de la venta
            var ventaTotal = ventaData.ventaTotal;
            // ...

            // En lugar de redirigir, devolvemos una respuesta en formato JSON
            return Ok(new { success = true });
        }

        public class Producto
        {
            public int id { get; set; }
            public int cantidad { get; set; }
            public decimal precio { get; set; }
            public decimal total { get; set; }
        }

        public class VentaData
        {
            public List<Producto> productos { get; set; }
            public decimal ventaTotal { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposVenta()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Ventas/GetVentaTipoVentaCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tiposVenta = JsonConvert.DeserializeObject<TiposVentaResponse>(content);
                return Json(tiposVenta.result);
            }
            return Json(null);
        }
        [HttpGet]
        public async Task<IActionResult> GetModelos()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudModeloCrudCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }
        private class TiposVentaResponse
        {
            public TiposVentaItem[] result { get; set; }
        }

        private class TiposVentaItem
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
    }
}
