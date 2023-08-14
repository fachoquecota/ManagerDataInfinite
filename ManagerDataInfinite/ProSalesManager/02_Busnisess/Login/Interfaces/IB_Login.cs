using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;

namespace ProSalesManager._02_Busnisess.Login.Interfaces
{
    public interface IB_Login
    {
        DBBoolResult LoginValitation(LoginModel loginModel);
    }
}
