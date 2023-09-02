using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace MVCManager.Controllers
{
    public class ProductosController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int pagina = 1)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudProductos");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var productosResponse = JsonConvert.DeserializeObject<ProductosResponseModel>(content);
                var productos = productosResponse.Products;

                int registrosPorPagina = 10;
                var productosPaginados = productos.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
                ViewBag.PaginaActual = pagina;
                ViewBag.TotalPaginas = Math.Ceiling((double)productos.Count / registrosPorPagina);

                return View(productosPaginados);
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudProveedorCrudCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetGeneros()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudGeneroCrudCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetCrudCategoriaCrudCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductoById(int idProducto)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5172/api/Productos/GetCrudProductoById?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProducto(ProductoModel model, IFormFile nuevaImagen)
        {
            if (Request.Form.ContainsKey("activo"))
            {
                model.Activo = true;
            }
            else
            {
                model.Activo = false;
            }
            if (nuevaImagen != null)
            {
                var folderPath = Path.Combine("wwwroot", "images", "ProductoPrincipal", model.IdProducto.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = ContentDispositionHeaderValue.Parse(nuevaImagen.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await nuevaImagen.CopyToAsync(stream);
                }

                model.ImagenCarpeta = $"images\\ProductoPrincipal\\{model.IdProducto}\\";
                model.ImagenNombre = fileName;
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5172/api/Productos/PutCrudProducto", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
