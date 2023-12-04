using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Sales.Interfaces;
using ProSalesManager._03_Models;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IB_Ventas _Ventas;

        public VentasController(IB_Ventas Ventas)
        {
            _Ventas = Ventas;

        }
        [HttpGet]
        [Route("GetVentaTipoVentaCB")]
        public dynamic GetVentaTipoVentaCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Ventas.TipoVentaCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de TipoVenta" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetProductosVenta")]
        public dynamic GetProductosVenta()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Ventas.ObtenerProductosVenta();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de productos" });

            return new
            {
                result = result
            };
        }

        [HttpPost]
        [Route("PostVenta")]
        public dynamic InsertSize(Venta venta)
        {
            var result = _Ventas.InsertarVentaConDetalles(venta);

            if (result is false)
                return BadRequest(new { message = "SE INSERTO" });

            return new
            {
                result = result
            };
        }

        [HttpGet]
        [Route("GetVentas")]
        public dynamic GetVentas()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Ventas.ObtenerVentas();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de TipoVenta" });

            return new
            {
                result = result
            };
        }

    }
}
