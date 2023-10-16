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
using Microsoft.AspNetCore.Hosting;


namespace MVCManager.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly HttpClient _httpClient;

        public ProductosController(HttpClient httpClient, IWebHostEnvironment hostingEnvironment)
        {
            _httpClient = httpClient;
            _hostingEnvironment = hostingEnvironment;
        }

        //Obtener productos
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

        public class ImageData
        {
            public string Image { get; set; }
            public string Type { get; set; }
        }

        public class TempImage
        {
            public Guid Id { get; set; }
            public byte[] ImageBytes { get; set; }
            public string Type { get; set; }
        }
        private static List<TempImage> _tempImages = new List<TempImage>();


        [HttpPost]
        public IActionResult LoadImageToMemory([FromBody] ImageData data)
        {
            try
            {
                var imageBytes = Convert.FromBase64String(data.Image);
                var imageId = Guid.NewGuid();
                _tempImages.Add(new TempImage { Id = imageId, ImageBytes = imageBytes, Type = data.Type });

                return Json(new { success = true, imageId = imageId });
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error de una forma más adecuada si lo deseas
                return Json(new { success = false, message = ex.Message });
            }
        }
        private static List<int> _deletedImageIds = new List<int>();

        // Método para agregar un ID de imagen a la lista de imágenes eliminadas
        [HttpPost("DeleteImageFromMemory")]
        public IActionResult DeleteImageFromMemory([FromBody] int imageId)
        {
            if (!_deletedImageIds.Contains(imageId))
            {
                _deletedImageIds.Add(imageId);
            }

            // Puedes agregar aquí lógica adicional si es necesario, por ejemplo, 
            // para guardar los IDs en una base de datos o realizar otras operaciones.

            return Ok(new { success = true });
        }
        public class ImageIdModel
        {
            public int ImageId { get; set; }
        }
        // Método opcional para obtener la lista de IDs de imágenes eliminadas (sólo para pruebas)
        [HttpPost]
        public IActionResult DeleteImageFromMemory([FromBody] ImageIdModel imageModel)
        {
            if (!_deletedImageIds.Contains(imageModel.ImageId))
            {
                _deletedImageIds.Add(imageModel.ImageId);
            }
            return Ok(_deletedImageIds);
        }


        public class TagUpdateInfo
        {
            public List<TagInfo> tagsToUpdate { get; set; }
            public List<TagInfo> tagsToAdd { get; set; }
            public List<TagToDelete> tagsToDelete { get; set; }
        }

        public class TagInfo
        {
            public string idTag { get; set; }
            public string description { get; set; }
            public bool isActive { get; set; }
        }

        public class TagToDelete
        {
            public string idTag { get; set; }
        }



        [HttpPost]
        public async Task<IActionResult> UpdateProducto(ProductoModel model, IFormFile nuevaImagen, string SizeUpdateorDelete, string TagUpdateInfo)
        {
            //TAG
            // Deserializar la información de los tags.
            var tagUpdateInfo = JsonConvert.DeserializeObject<TagUpdateInfo>(TagUpdateInfo);

            // Luego, puedes acceder a las listas de tags actualizados, nuevos y eliminados:
            var tagsToUpdate = tagUpdateInfo.tagsToUpdate;
            var tagsToCreate = tagUpdateInfo.tagsToAdd;
            var tagsToDelete = tagUpdateInfo.tagsToDelete;

            // Actualizar tags
            using (var httpClient = new HttpClient()) {
                foreach (var tag in tagsToUpdate)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        idTags = tag.idTag,
                        idProducto = model.IdProducto, // Asume que tienes el IdProducto en tu modelo.
                        descripcion = tag.description,
                        activo = tag.isActive
                    }), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync("http://localhost:5172/api/Productos/PutCrudTag", content);
                    // Puedes manejar la respuesta si lo necesitas.
                }
            }



            // Crear nuevos tags
            using (var httpClient = new HttpClient())
            {
                foreach (var tag in tagsToCreate)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        idTags = 0,
                        idProducto = model.IdProducto,
                        descripcion = tag.description,
                        activo = tag.isActive
                    }), Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("http://localhost:5172/api/Productos/PostCrudTag", content);
                    // Puedes manejar la respuesta si lo necesitas.
                }
            }
            // Eliminar tags
            using (var httpClient = new HttpClient())
            {
                foreach (var tag in tagUpdateInfo.tagsToDelete)
                {
                    var response = await httpClient.DeleteAsync($"http://localhost:5172/api/Productos/DeleteCrudTag?id={tag.idTag}");
                    // Puedes manejar la respuesta si lo necesitas.
                }
            }
            // SIZE
            // 1. Deserializar SizeUpdateorDelete
            var sizeData = JsonConvert.DeserializeObject<dynamic>(SizeUpdateorDelete);
            var toUpdate = sizeData.toUpdate.ToObject<List<dynamic>>();
            var toCreate = sizeData.toCreate.ToObject<List<dynamic>>();
            var deleted = sizeData.deletedItems.ToObject<List<int>>();

            // 2. eliminar registros

            foreach (var id in deleted)
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.DeleteAsync($"http://localhost:5172/api/Productos/DeleteCrudSizeDetalle?idSizeDetalle={id}");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Imagen con idSizeDetalle = {id} eliminada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine($"Error al eliminar la imagen con idSizeDetalle = {id}. Respuesta: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }

            // 3. Procesar registros a actualizar
            foreach (var record in toUpdate)
            {
                var idSizeDetalle = Convert.ToInt32(record.idSizeDetalle);
                var idSize = Convert.ToInt32(record.idSize);
                var activo = Convert.ToBoolean(record.activo);

                using (var httpClient = new HttpClient())
                {
                    var updateData = new
                    {
                        idSizeDetalle = idSizeDetalle,
                        idSize = idSize,
                        idProducto = model.IdProducto, // Asumo que el IdProducto lo obtienes del modelo, si no es así, ajusta esta línea.
                        descripcion = "Test por api",
                        activo = activo
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(updateData), Encoding.UTF8, "application/json");
                    var response = await httpClient.PutAsync("http://localhost:5172/api/Productos/PutCrudSizeDetalle", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"SizeDetalle con idSizeDetalle = {idSizeDetalle} actualizado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine($"Error al actualizar SizeDetalle con idSizeDetalle = {idSizeDetalle}. Respuesta: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }

            }

            // 4. Procesar nuevos registros
            foreach (var record in toCreate)
            {
                var idSize = record.idSize;
                var activo = record.activo;

                using (var httpClient = new HttpClient())
                {
                    var newData = new
                    {
                        idSizeDetalle = 0,
                        idSize = idSize,
                        idProducto = model.IdProducto, // Supongo que el IdProducto proviene del modelo, ajusta si es necesario.
                        descripcion = "test api insert",
                        activo = activo
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(newData), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("http://localhost:5172/api/Productos/PostCrudSizeDetalle", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Nuevo SizeDetalle con idSize = {idSize} agregado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine($"Error al agregar SizeDetalle con idSize = {idSize}. Respuesta: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }

            }


            // IMAGENES
            // 1. Imprimir los IDs de las imágenes eliminadas
            if (_deletedImageIds.Count > 0)
            {
                Console.WriteLine("ID de imágenes eliminadas:");
                foreach (int imageId in _deletedImageIds)
                {
                    Console.WriteLine(imageId);

                    // Eliminar la imagen usando la API
                    var response = await _httpClient.DeleteAsync($"http://localhost:5172/api/Productos/DeleteImagen?oImagenModel={imageId}");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Imagen con ID {imageId} eliminada con éxito a través de la API.");
                    }
                    else
                    {
                        Console.WriteLine($"Hubo un error al eliminar la imagen con ID {imageId} a través de la API.");
                    }
                }
                // Limpiar la lista de ID de imágenes eliminadas una vez que se hayan manejado
                _deletedImageIds.Clear();
            }
            else
            {
                Console.WriteLine("No se eliminaron imágenes.");
            }

            // 2. Manejar y mostrar la ruta de las imágenes nuevas
            if (_tempImages.Count > 0)
            {
                var folderPath = Path.Combine("wwwroot", "Images", "ProductoDetalle", model.IdProducto.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                Console.WriteLine("Imágenes nuevas:");
                foreach (var imgData in _tempImages)
                {
                    var fileExtension = imgData.Type switch
                    {
                        "image/jpeg" => ".jpg",
                        "image/png" => ".png",
                        _ => ".jpg", // Valor predeterminado
                    };

                    var fileName = $"{Guid.NewGuid()}{fileExtension}";  // Genera un nombre único basado en Guid
                    var fullPath = Path.Combine(folderPath, fileName);

                    await System.IO.File.WriteAllBytesAsync(fullPath, imgData.ImageBytes);

                    Console.WriteLine($"Imagen almacenada en: {fullPath}");

                    // Enviar la imagen a la API
                    var imageToPost = new
                    {
                        idImagenes = 0,
                        idProducto = model.IdProducto,
                        rutaImagen = $"images\\ProductoDetalle\\{model.IdProducto}\\",
                        nombreImagen = fileName,
                        activo = true
                    };

                    var json = JsonConvert.SerializeObject(imageToPost);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync("http://localhost:5172/api/Productos/PostCrudImagen", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Imagen enviada a la API con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("Hubo un error al enviar la imagen a la API.");
                    }
                }

                // Limpiar las imágenes en memoria una vez enviadas
                _tempImages.Clear();
            }
            else
            {
                Console.WriteLine("No se añadieron imágenes nuevas.");
            }


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
            var jsonProducto = JsonConvert.SerializeObject(model);
            Console.WriteLine(jsonProducto);
            var contentProducto = new StringContent(jsonProducto, Encoding.UTF8, "application/json");
            var readContent = await contentProducto.ReadAsStringAsync();
            Console.WriteLine(readContent);

            var responseA = await _httpClient.PutAsync("http://localhost:5172/api/Productos/PutCrudProducto", contentProducto);

            if (responseA.IsSuccessStatusCode)
            {
                // El API procesó la solicitud correctamente
                //var responseBody = await responseA.Content.ReadAsStringAsync();
                var responseBody = await responseA.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                Console.WriteLine("Respuesta del API: " + responseBody);
            }
            else
            {
                // Hubo un error al procesar la solicitud
                Console.WriteLine("Error: " + responseA.StatusCode);
            }

            if (responseA.IsSuccessStatusCode)
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

        [HttpGet]
        public async Task<IActionResult> GetTagsById(int idProducto)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5172/api/Productos/GetCrudTagById?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetCrudColorDetalleById(int idProducto)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5172/api/Productos/GetCrudColorDetalleById?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return Json(new { success = false });
        }
        [HttpGet]
        public async Task<IActionResult> GetColor_CrudCB()
        {
            var response = await _httpClient.GetAsync("http://localhost:5172/api/Productos/GetColor_CrudCB");
            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public async Task<IActionResult> UploadImages(int idProducto, List<IFormFile> files)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, $"Images/ProductoDetalle/{idProducto}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { status = "Imágenes subidas con éxito" });
        }

        [HttpGet]
        public async Task<IActionResult> GetCrudImagenById(int idProducto)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5172/api/Productos/GetCrudImagenById?idProducto={idProducto}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                 return Content(content, "application/json");
            }
            return Json(new { success = false });
        }


    }
}
