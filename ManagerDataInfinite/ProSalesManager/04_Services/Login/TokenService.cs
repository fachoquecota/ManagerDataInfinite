using ProSalesManager._03_Models;
using ProSalesManager._04_Services.Login.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace ProSalesManager._04_Services.Login
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(LoginModel admin)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.email)
            };

            var keyString = _configuration.GetSection("JWT:Key").Value;
            var keyBytes = Encoding.UTF8.GetBytes(keyString);

            // Ensure key has at least 256 bits
            if (keyBytes.Length < 32)
            {
                throw new Exception("Invalid key size. Key must have at least 256 bits.");
            }

            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityTokens = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1400),
                signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(securityTokens);
            return token;
        }
        public string GetCorreoFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Acceder al correo electrónico desde el token
            var correo = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            return correo;
        }
    }
}