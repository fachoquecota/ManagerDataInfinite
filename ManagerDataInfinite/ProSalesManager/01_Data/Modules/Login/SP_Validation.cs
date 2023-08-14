using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Login.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;
namespace ProSalesManager._01_Data.Modules.Login
{
    public class SP_Validation : ISP_Login
    {
        public List<DBBoolResult> LoginValitation(LoginModel loginModel)
        {
            //* Utilizamos DBBoolResult de tipo Models para recepcionar la información porque
            //* cabe la posibilidad de que se aumenten más columnas en la respuesta del procedimiento almacenado
            //* es posible usar otro método para aumentar las columnas sin necesida de cambiar este código
            var oList = new List<DBBoolResult>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_LoginValidator", conexion);
                    cmd.Parameters.AddWithValue("@vcCorreo", loginModel.email);
                    cmd.Parameters.AddWithValue("@vcPassword", loginModel.Password);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new DBBoolResult()
                            {
                                result = Convert.ToInt32(dr["Result"]),
                            });
                        }
                    }
                }
                return oList;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                oList.Add(new DBBoolResult()
                {
                    result = 0,
                    value = ex.Message
                });
                return oList;
            }
        }
    }
}
