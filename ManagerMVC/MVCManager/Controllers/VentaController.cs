using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCManager.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using static MVCManager.Controllers.ClienteController;

namespace MVCManager.Controllers
{
    public class VentaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public VentaController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index(int pagina = 1)
        {
            // Verificar si el usuario está autenticado (verificar la existencia del token)
            var accessToken = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(accessToken))
            {
                // Si no hay token, redirigir al usuario a la página de inicio de sesión
                return RedirectToAction("Index", "Login");
            }
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(baseUrl+"api/Ventas/GetProductosVenta");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<ProductoVenta>>>(content);
                var productosList = responseDictionary["result"];

                var productos = productosList.OrderByDescending(p => p.IdProducto).ToList();

                HttpResponseMessage ubigeoResponse = await httpClient.GetAsync(baseUrl+"api/Ventas/GetUbigeo");
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

                HttpResponseMessage colorResponse = await httpClient.GetAsync(baseUrl+"api/Productos/GetColor_CrudCB");
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

                HttpResponseMessage sizeResponse = await httpClient.GetAsync(baseUrl+"api/Size/GetAllSizes");
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

                HttpResponseMessage modeloResponse = await httpClient.GetAsync(baseUrl+"api/Productos/GetCrudModeloCrudCB");
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

                HttpResponseMessage calidadResponse = await httpClient.GetAsync(baseUrl+"api/Calidad/GetAllCalidadCombobox");
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

                HttpResponseMessage marcaResponse = await httpClient.GetAsync(baseUrl+"api/Marca/GetAllMarcaCombobox");
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

                HttpResponseMessage categoriaResponse = await httpClient.GetAsync(baseUrl+"api/Productos/GetCrudCategoriaCrudCB");
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

                HttpResponseMessage clienteResponse = await httpClient.GetAsync(baseUrl+"api/Clientes/GetClientes");
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

                HttpResponseMessage tipoResponse = await httpClient.GetAsync(baseUrl+"api/Ventas/GetVentaTipoVentaCB");
                if (tipoResponse.IsSuccessStatusCode)
                {
                    var tipoContent = await tipoResponse.Content.ReadAsStringAsync();
                    var tipoData = JsonConvert.DeserializeObject<CategoriaResponse>(tipoContent);

                    ViewBag.TipoVenta = tipoData.result.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Descripcion
                    }).ToList();
                }

                HttpResponseMessage tipoDocumentoResponse = await httpClient.GetAsync(baseUrl+"api/Clientes/GetTipoDocumento_ComboBox");
                if (tipoDocumentoResponse.IsSuccessStatusCode)
                {
                    var tipoDocumentoContent = await tipoDocumentoResponse.Content.ReadAsStringAsync();
                    var tipoDocumentoData = JsonConvert.DeserializeObject<Dictionary<string, List<TipoDocumento>>>(tipoDocumentoContent);
                    var tipoDocumento = tipoDocumentoData["result"];

                    ViewBag.TipoDocumentos = tipoDocumento.Select(c => new SelectListItem
                    {
                        Value = c.id.ToString(),
                        Text = c.descripcion
                    }).ToList();
                }

                HttpResponseMessage transResponse = await httpClient.GetAsync(baseUrl+"api/EmpresaTransporte/GetAllTransporteCombobox");
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

                //return View();  // Devuelve la vista correspondiente.
                //int registrosPorPagina = 10;
                //var productosPaginados = productos.Skip((pagina - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();
                //ViewBag.PaginaActual = pagina;
                //ViewBag.TotalPaginas = Math.Ceiling((double)productos.Count / registrosPorPagina);

                return View(productos);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarVenta([FromBody] VentaFinal ventaData)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();
            // Código antes de la espera
            //await Task.Delay(5000);


            //return Ok(new { success = true });
            // Asegúrate de que ventaData no es nulo
            if (ventaData == null)
            {
                ViewBag.ErrorMessage = "Los datos de la venta son inválidos o están incompletos.";
                return View("Error"); // Cambia a una vista que maneje el error.
            }

            ventaData.fechaVenta = DateTime.Now;
            //ventaData.idCliente = 1;
            ventaData.idUsuario = 1;
            ventaData.idEmpresa = 1;
            var json = JsonConvert.SerializeObject(ventaData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);
            var response = await httpClient.PostAsync(baseUrl+"api/Ventas/PostVenta", content);

            if (response.IsSuccessStatusCode)
            {
                var printableContent = GeneratePrintableContent(ventaData);
                return Json(new { success = true, content = printableContent });
            }
            else
            {
                ViewBag.ErrorMessage = "Error al procesar la venta. Intente nuevamente.";
                return View("Error");
            }

        }

        private string GeneratePrintableContent(VentaFinal ventaData)
        {
            // Create an HTML template for the ticket
            var ticketHtml = @"
                <html>
                <head>
                    <style>
                        body {
                            width: 55mm;
                            font-family: 'Arial', sans-serif;
                            font-size: 12px;
                            line-height: 1.5;
                            margin: 0;
                        }
                        .ticket {
                            width: 100%;
                            text-align: center;
                        }
                        /* Aquí puedes agregar más estilos según sea necesario */
                    </style>
                </head>
                <body>
                    <div class='ticket'>
                        <h1>Ticket de Venta</h1>
                        <table>
                            <!-- Aquí van los detalles de la venta -->
                        </table>
                        <p>Total: $TOTAL_AMOUNT</p>
                    </div>
                </body>
                </html>";

            // Populate the table rows with product details
            var rows = "";
            foreach (var detalle in ventaData.detallesVenta)
            {
                var row = $"<tr><td>{detalle.idProducto}</td><td>{detalle.cantidad}</td><td>${detalle.precio}</td><td>${detalle.cantidad * detalle.precio}</td></tr>";
                rows += row;
            }

            // Replace the placeholder with the total amount
            ticketHtml = ticketHtml.Replace("$TOTAL_AMOUNT", ventaData.totalDefinido.ToString());

            // Replace the <!-- Loop through ventaData... --> placeholder with the generated rows
            ticketHtml = ticketHtml.Replace("<!-- Loop through ventaData... -->", rows);

            return ticketHtml;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposVenta()
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await httpClient.GetAsync(baseUrl+"api/Ventas/GetVentaTipoVentaCB");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tiposVenta = JsonConvert.DeserializeObject<TiposVentaResponse>(content);
                return Json(tiposVenta.result);
            }
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCliente([FromBody] Cliente cliente)
        {
            var baseUrl = _configuration["OriginPathApi"];
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.PostAsJsonAsync(baseUrl+"api/Clientes/PostClientes", cliente);

            if (response.IsSuccessStatusCode)
            {
                // Manejar la respuesta exitosa
                return Ok();
            }
            else
            {
                // Manejar el error
                return BadRequest();
            }
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
        public class UbigeoModel
        {
            public int IdUbigeo { get; set; }
            public string Departamento { get; set; }
            public string Provincia { get; set; }
            public string Distrito { get; set; }
        }
        public class ApiResponseUbigeo
        {
            public List<UbigeoModel> Result { get; set; }
        }
        public class VentaData
        {
            public DateTime fechaVenta { get; set; }
            public int idCliente { get; set; }
            public int idUsuario { get; set; }
            public int idEmpresa { get; set; }
            public int idTipoPago { get; set; }
            public int? totalDefinido { get; set; }
            public List<DetalleVenta> detallesVenta { get; set; }
        }
        public class DetalleVenta
        {
            public int idProducto { get; set; }
            public int cantidad { get; set; }
            public decimal precioUnitario { get; set; }
        }
        public class Ubicacion
        {
            public string departamento { get; set; }
            public string provincia { get; set; }
            public string distrito { get; set; }
        }
        public class ProductoVentaFinal
        {
            public int idProducto { get; set; }
            public int cantidad { get; set; }
            public decimal precio { get; set; }
        }
        public class VentaFinal
        {
            public DateTime fechaVenta { get; set; }
            public int idCliente { get; set; }
            public int idUsuario { get; set; }
            public int idEmpresa { get; set; }
            public int idTipoPago { get; set; }
            public int idTransporteCombobox { get; set; }
            public Ubicacion ubicacion { get; set; } 
            public decimal? totalDefinido { get; set; }
            public List<ProductoVentaFinal> detallesVenta { get; set; }
        }
        public class TipoDocumento
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
    }
}
