using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Supplier.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Supplier
{
    public class SP_Proveedores : ISP_Proveedores
    {
        public List<ProveedoresModel> ProveedoresLista(string usuarioNavegacion)
        {
            //* Utilizamos DBBoolResult de tipo Models para recepcionar la información porque
            //* cabe la posibilidad de que se aumenten más columnas en la respuesta del procedimiento almacenado
            //* es posible usar otro método para aumentar las columnas sin necesida de cambiar este código
            var oList = new List<ProveedoresModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@vcCorreo", usuarioNavegacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ProveedoresModel()
                            {
                                idProveedor = Convert.ToInt32(dr["idProveedor"]),
                                proveedor = dr["proveedor"].ToString(),
                                contacto = dr["contacto"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                direccion = dr["direccion"].ToString(),
                                fecha = Convert.ToDateTime(dr["fecha"]),
                                horaCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                idEmpresa = Convert.ToInt32(dr["idEmpresa"])
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
        public bool UpdateProveedor(ProveedoresModel oProveedoresModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                DateTime datetime = DateTime.Now;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oProveedoresModel.idProveedor);
                    cmd.Parameters.AddWithValue("@vcProveedor", oProveedoresModel.proveedor);
                    cmd.Parameters.AddWithValue("@vcContacto", oProveedoresModel.contacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oProveedoresModel.telefono);
                    cmd.Parameters.AddWithValue("@vcDireccion", oProveedoresModel.direccion);
                    cmd.Parameters.AddWithValue("@vcFecha", oProveedoresModel.fecha);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProveedoresModel.idEmpresa);

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
        public bool InsertProveedor(ProveedoresModel oProveedoresModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                DateTime datetime = DateTime.Now;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@vcProveedor", oProveedoresModel.proveedor);
                    cmd.Parameters.AddWithValue("@vcContacto", oProveedoresModel.contacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oProveedoresModel.telefono);
                    cmd.Parameters.AddWithValue("@vcDireccion", oProveedoresModel.direccion);
                    cmd.Parameters.AddWithValue("@vcFecha", oProveedoresModel.fecha);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProveedoresModel.idEmpresa);

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
