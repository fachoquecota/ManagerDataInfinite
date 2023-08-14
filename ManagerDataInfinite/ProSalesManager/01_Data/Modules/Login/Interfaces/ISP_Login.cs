using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;

namespace ProSalesManager._01_Data.Modules.Login.Interfaces
{
    public interface ISP_Login
    {
        public List<DBBoolResult> LoginValitation(LoginModel loginModel);
    }
}
