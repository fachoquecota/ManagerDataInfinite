using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCManager.Controllers
{
    public class VentaController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<IActionResult> Index(int pagina = 1)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Ventas/GetProductosVenta");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ProductoVenta>>>(content);
                var productosList = responseDictionary["result"];

                var productos = productosList.OrderByDescending(p => p.IdProducto).ToList();

                HttpResponseMessage colorResponse = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Productos/GetColor_CrudCB");
                if (colorResponse.IsSuccessStatusCode)
                {
                    var colorContent = await colorResponse.Content.ReadAsStringAsync();
                    var colorData = JsonConvert.DeserializeObject<Dictionary<string, List<Colores>>>(colorContent);
                    var colores = colorData["result"];

                    ViewBag.Colores = colores.Select(c => new SelectListItem
                    {
                        Value = c.id.ToString(),
                        Text = c.descripcion
                    }).ToList();
                }

                // Llamada a la API para obtener las tallas
                HttpResponseMessage sizeResponse = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Size/GetAllSizes");
                if (sizeResponse.IsSuccessStatusCode)
                {
                    var sizeContent = await sizeResponse.Content.ReadAsStringAsync();
                    var sizeData = JsonConvert.DeserializeObject<Dictionary<string, List<Talla>>>(sizeContent);
                    var tallas = sizeData["products"];

                    // Filtrar las tallas que tienen "activo" igual a true
                    var tallasActivas = tallas.Where(t => t.activo).ToList();

                    // Llenar el ViewBag con las tallas activas
                    ViewBag.Tallas = tallasActivas.Select(t => new SelectListItem
                    {
                        Value = t.idSize.ToString(),
                        Text = t.descripcion
                    }).ToList();
                }
                HttpResponseMessage modeloResponse = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Productos/GetCrudModeloCrudCB");
                if (modeloResponse.IsSuccessStatusCode)
                {
                    var modeloContent = await modeloResponse.Content.ReadAsStringAsync();
                    var modeloData = JsonConvert.DeserializeObject<ModeloResponse>(modeloContent);

                    ViewBag.Modelos = modeloData.Result.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }

                HttpResponseMessage calidadResponse = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Calidad/GetAllCalidadCombobox");
                if (calidadResponse.IsSuccessStatusCode)
                {
                    var calidadContent = await calidadResponse.Content.ReadAsStringAsync();
                    var calidadData = JsonConvert.DeserializeObject<CalidadResponse>(calidadContent);

                    ViewBag.Calidad = calidadData.result.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }

                HttpResponseMessage marcaResponse = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Marca/GetAllMarcaCombobox");
                if (marcaResponse.IsSuccessStatusCode)
                {
                    var marcaContent = await marcaResponse.Content.ReadAsStringAsync();
                    var marcaData = JsonConvert.DeserializeObject<MarcaResponse>(marcaContent);

                    ViewBag.Marca = marcaData.result.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Descripcion
                    }).ToList();
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

                //return View();  // Devuelve la vista correspondiente.
                int registrosPorPagina = 10;
                var productosPaginados = productos.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
                ViewBag.PaginaActual = pagina;
                ViewBag.TotalPaginas = Math.Ceiling((double)productos.Count / registrosPorPagina);

                return View(productosPaginados);
            }

            return View("Error");
        }


        public class Talla
        {
            public int idSize { get; set; }
            public string descripcion { get; set; }
            public bool activo { get; set; }
        }

        public class Colores
        {
            public int id { get; set; }
            public string descripcion { get; set; }
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
            HttpResponseMessage response = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Ventas/GetVentaTipoVentaCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tiposVenta = JsonConvert.DeserializeObject<TiposVentaResponse>(content);
                return Json(tiposVenta.result);
            }
            return Json(null);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetModelos()  // Cambia 'TuActionDeVista' por el nombre real del método que renderiza tu vista.
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync("http://apiprosalesmanager.somee.com/api/Productos/GetCrudModeloCrudCB");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();
        //        var modeloResponse = JsonConvert.DeserializeObject<ModeloResponse>(content);
        //        ViewBag.Modelos = modeloResponse.Result;
        //    }
        //    // Código para obtener y asignar colores y tallas a ViewBag si es necesario...

        //    return View();  // Devuelve la vista correspondiente.
        //}


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
