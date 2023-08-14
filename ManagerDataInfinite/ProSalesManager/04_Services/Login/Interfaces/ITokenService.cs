using ProSalesManager._03_Models;

namespace ProSalesManager._04_Services.Login.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginModel admin);
        string GetCorreoFromToken(string token);
    }
}
