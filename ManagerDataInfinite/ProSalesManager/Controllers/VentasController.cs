using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Sales.Interfaces;

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
    }
}
