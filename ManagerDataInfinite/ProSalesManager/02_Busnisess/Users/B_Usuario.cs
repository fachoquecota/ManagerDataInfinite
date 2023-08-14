using ProSalesManager._01_Data.Modules.Login.Interfaces;
using ProSalesManager._01_Data.Modules.Users.Interfaces;
using ProSalesManager._02_Busnisess.Users.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.DataBodyModels;

namespace ProSalesManager._02_Busnisess.Users
{
    public class B_Usuario : IB_Usuario
    {
        private readonly ISP_Usuario _sP_Usuario;
        public B_Usuario(ISP_Usuario sP_Usuario)
        {
            _sP_Usuario = sP_Usuario;
        }
        public UsuarioModel UsuarioDatos(string sendUsuarioInicioModel)
        {        
            var resultSP = _sP_Usuario.GetUsuario(sendUsuarioInicioModel);
            return resultSP;
        }
    }
}
