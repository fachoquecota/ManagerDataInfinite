using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Customers.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Customers
{
    public class SP_Clientes : ISP_Clientes
    {
        public List<ClienteModel> ClientesLista(string usuarioNavegacion)
        {
            //* Utilizamos DBBoolResult de tipo Models para recepcionar la información porque
            //* cabe la posibilidad de que se aumenten más columnas en la respuesta del procedimiento almacenado
            //* es posible usar otro método para aumentar las columnas sin necesida de cambiar este código
            var oList = new List<ClienteModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Clientes", conexion);
                    cmd.Parameters.AddWithValue("@vcCorreo", usuarioNavegacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ClienteModel()
                            {
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombreContacto = dr["nombreContacto"].ToString(),
                                apellidoContacto = dr["apellidoContacto"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                idTipoDocumento = Convert.ToInt32(dr["idTipoDocumento"]),
                                numeroDocumento = dr["numeroDocument"].ToString(),
                                razonSocial = dr["razonSocial"].ToString(),
                                nombreComercial = dr["nombreComercia"].ToString(),
                                correo = dr["correo"].ToString(),
                                direccion = dr["direccion"].ToString(),
                                idEmpresa = Convert.ToInt32(dr["idEmpresa"]),
                            });
                        }
                    }
                }
                return oList;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return oList;
            }
        }
        public bool UpdateCliente(ClienteModel oClienteModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                DateTime datetime = DateTime.Now;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Clientes", conexion);
                    cmd.Parameters.AddWithValue("@inIdCliente", oClienteModel.idCliente);
                    cmd.Parameters.AddWithValue("@vcNombreContacto", oClienteModel.nombreContacto);
                    cmd.Parameters.AddWithValue("@vcApellidoContacto", oClienteModel.apellidoContacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oClienteModel.telefono);
                    cmd.Parameters.AddWithValue("@inIdTipoDocumento", oClienteModel.idTipoDocumento);
                    cmd.Parameters.AddWithValue("@vcNumeroDocumento", oClienteModel.numeroDocumento);
                    cmd.Parameters.AddWithValue("@vcRazonSocial", oClienteModel.razonSocial);
                    cmd.Parameters.AddWithValue("@vcNombreComercial", oClienteModel.nombreComercial);
                    cmd.Parameters.AddWithValue("@vcCorreo", oClienteModel.correo);
                    cmd.Parameters.AddWithValue("@vcDireccion", oClienteModel.direccion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return rpta;
            }
            return rpta;
        }
        public bool InsertCliente(ClienteModel oClienteModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                DateTime datetime = DateTime.Now;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Clientes", conexion);
                    cmd.Parameters.AddWithValue("@vcNombreContacto", oClienteModel.nombreContacto);
                    cmd.Parameters.AddWithValue("@vcApellidoContacto", oClienteModel.apellidoContacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oClienteModel.telefono);
                    cmd.Parameters.AddWithValue("@inIdTipoDocumento", oClienteModel.idTipoDocumento);
                    cmd.Parameters.AddWithValue("@vcNumeroDocumento", oClienteModel.numeroDocumento);
                    cmd.Parameters.AddWithValue("@vcRazonSocial", oClienteModel.razonSocial);
                    cmd.Parameters.AddWithValue("@vcNombreComercial", oClienteModel.nombreComercial);
                    cmd.Parameters.AddWithValue("@vcCorreo", oClienteModel.correo);
                    cmd.Parameters.AddWithValue("@vcDireccion", oClienteModel.direccion);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oClienteModel.idEmpresa);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return rpta;
            }
            return rpta;
        }
    }
}
