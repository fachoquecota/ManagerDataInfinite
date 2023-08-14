using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Customers.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IB_Clientes _Clientes;
        private readonly ITokenService _tokenService;
        public ClientesController(IB_Clientes Clientes, ITokenService tokenService)
        {
            _Clientes = Clientes;
            _tokenService = tokenService;
        }
        [HttpGet]
        [Route("GetClientes")]
        public dynamic GetProducts()
        {
            var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Clientes.Clientes(correo);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de clientes" });

            return new
            {
                //message = jwtToken
                message = result
            };
        }
        [HttpPut]
        [Route("PutClientes")]
        public dynamic PutProducts([FromBody] ClienteModel oClienteModel)
        {
            bool result = _Clientes.UpdateCliente(oClienteModel);
            return Ok(result);
        }
        [HttpPost]
        [Route("PostClientes")]
        public dynamic PostProducts([FromBody] ClienteModel oClienteModel)
        {
            bool result = _Clientes.InsertCliente(oClienteModel);
            return Ok(result);
        }
    }
}
