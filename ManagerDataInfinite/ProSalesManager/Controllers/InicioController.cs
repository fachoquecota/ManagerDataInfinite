using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Users.Interfaces;
using ProSalesManager._03_Models.DataBodyModels;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InicioController : ControllerBase
    {
        private readonly IB_Usuario _Usuario;
        private readonly ITokenService _tokenService;
        public InicioController(IB_Usuario Usuario, ITokenService tokenService)
        {
            _Usuario = Usuario;
            _tokenService = tokenService;   
        }
        [HttpGet]
        [Route("GetUsuarioInicio")]
        public dynamic GetProducts()
        {
            var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Usuario.UsuarioDatos(correo);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de usuario" });

            return new
            {
                //message = jwtToken
                message = result
            };
        }
    }
}
