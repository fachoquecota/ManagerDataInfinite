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
using Newtonsoft.Json.Linq;

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
        private async Task<ProductoModel> FetchProductoById(int idProducto)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5172/api/Productos/GetCrudProductoById?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var jsonObject = JObject.Parse(content);
                    var resultArray = jsonObject["result"];
                    if (resultArray != null)
                    {
                        var firstResult = resultArray.FirstOrDefault();
                        if (firstResult != null)
                        {
                            var producto = firstResult.ToObject<ProductoModel>();
                            return producto;
                        }
                    }
                }
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProducto(ProductoModel model, IFormFile nuevaImagen, string TableData, string DeletedItems)
        {

            // Mantener los datos previos de la imagen si no se selecciona una nueva
            if (nuevaImagen == null)
            {
                var existingData = await FetchProductoById(model.IdProducto);
                if (existingData != null)
                {
                    model.ImagenCarpeta = existingData.ImagenCarpeta;
                    model.ImagenNombre = existingData.ImagenNombre;
                }
            }
            else
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

            // Código para manejar los datos de la tabla de tallas
            List<SizeRecordModel> toUpdate = new List<SizeRecordModel>();
            List<SizeRecordModel> toCreate = new List<SizeRecordModel>();
            List<string> toDelete = new List<string>();

            if (!string.IsNullOrEmpty(TableData))
            {
                dynamic tableData = JsonConvert.DeserializeObject(TableData);
                toUpdate = JsonConvert.DeserializeObject<List<SizeRecordModel>>(tableData.toUpdate.ToString());
                toCreate = JsonConvert.DeserializeObject<List<SizeRecordModel>>(tableData.toCreate.ToString());
                toDelete = JsonConvert.DeserializeObject<List<string>>(DeletedItems);

            }

            // Aquí puedes procesar las listas toUpdate y toCreate como necesites

            // Código para enviar los datos al API
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5172/api/Productos/PutCrudProducto", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProducto(int idProducto)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5172/api/Productos/DeleteCrudProducto?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetCrudSizeDetalleById(int idProducto)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5172/api/Productos/GetCrudSizeDetalleById?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            return BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> GetSize_CrudCB()
        {
            var response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetSize_CrudCB");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            return BadRequest();
        }

    }
}
