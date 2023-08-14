using ProSalesManager._01_Data.Modules.Login.Interfaces;
using ProSalesManager._02_Busnisess.Login.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;

namespace ProSalesManager._02_Busnisess.Login
{
    public class B_Login : IB_Login
    {
        private readonly ISP_Login _login;

        public B_Login(ISP_Login login)
        {
            _login = login;
        }
        public DBBoolResult LoginValitation(LoginModel loginModel)
        {
            var oList = new DBBoolResult();
            var resultSP = _login.LoginValitation(loginModel);
            bool dataSPBool = resultSP.Count > 0 && resultSP[0].result.Equals(1);
            if (dataSPBool == false)
            {
                oList.result = 0;
                oList.value = "Usuario o contraseña incorrecta";
                return oList;
            }
            else
            {
                oList.result = 1;
                oList.value = "Autenticación verificada!";
                return oList;
            }
        }
    }
}
