using ProSalesManager._03_Models;

namespace ProSalesManager._01_Data.Modules.Users.Interfaces
{
    public interface ISP_Usuario
    {
        UsuarioModel GetUsuario(string correo);
    }
}
