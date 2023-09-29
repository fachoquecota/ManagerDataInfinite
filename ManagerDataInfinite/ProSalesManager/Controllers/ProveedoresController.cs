using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Suppliers.Interfaces;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IB_Proveedores _Proveedores;
        private readonly ITokenService _tokenService;
        public ProveedoresController(IB_Proveedores Proveedores, ITokenService tokenService)
        {
            _Proveedores = Proveedores;
            _tokenService = tokenService;
        }
        [HttpGet]
        [Route("GetProveedores")]
        public dynamic GetProducts()
        {
            var correoToken= HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Proveedores.Proveedores(correo);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de clientes" });

            return new
            {
                //message = jwtToken
                message = result
            };
        }
        [HttpPut]
        [Route("PutProveedores")]
        public dynamic PutProducts([FromBody] ProveedoresModel oProveedoresModel)
        {
            bool result = _Proveedores.UpdateProveedor(oProveedoresModel);
            return Ok(result);
        }
        [HttpPost]
        [Route("PostProveedores")]
        public dynamic PostProducts([FromBody] ProveedoresModel oProveedoresModel)
        {
            bool result = _Proveedores.InsertProveedor(oProveedoresModel);
            return Ok(result);
        }
    }
}
