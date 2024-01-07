using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Login.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IB_Login _login;
        private readonly ITokenService _tokenService;

        public LoginController(IB_Login login, IConfiguration configuration, ITokenService tokenService)
        {
            _login = login; 
            _tokenService = tokenService;   
        }
        [HttpPost]
        [Route("GetTokenLogin")]
        public IActionResult GetToken([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest(new { message = "El modelo de inicio de sesión es nulo" });
            }

            var result = _login.LoginValitation(loginModel);
            if (result is null)
            {
                // Manejar el caso en que result es null
                return BadRequest(new { message = "Error en la validación del inicio de sesión" });
            }

            if (result.result == 1)
            {
                var response = _tokenService.GenerateToken(loginModel);
                return Ok(new
                {
                    result = "OK",
                    accessToken = response
                });
            }
            else
            {
                // En caso de que result no sea 1
                return BadRequest(new
                {
                    result = "NOOK"
                });
            }
        }

    }
}
