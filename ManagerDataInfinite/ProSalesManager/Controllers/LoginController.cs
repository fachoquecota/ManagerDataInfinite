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
        public dynamic GetToken([FromBody] LoginModel loginModel)
        {
            var result = _login.LoginValitation(loginModel);
            string response = "";
            if (result is null)
                return BadRequest(new { message = result });
            if (result.result == 1)
            {
                if (loginModel == null)
                {
                    return BadRequest(new { message = "El modelo de inicio de sesión es nulo" });
                }
                response = _tokenService.GenerateToken(loginModel);
                return new
                {
                    Result = result.value,
                    message = response
                };
            }
            else
            {
                return new
                {
                    Result = result.value
                };
            }
        }
    }
}
