using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Transporte.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Transporte
{
    public class SP_Transporte : ISP_Transporte
    {
        public List<ComboBox> ObtenerEmpresaTransporteComboBox()
        {
            var listaCalidades = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_empresaTransporte_Combobox", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaCalidades.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idEmpresaTranspte"]),
                                descripcion = dr["descripcion"].ToString(),
                            });
                        }
                    }
                }
                return listaCalidades;
            }
            catch (Exception ex)
            {
                // Suponiendo que ErrorResult es una clase que gestiona errores
                ErrorResult.ErrorMessage = ex.Message;
                return listaCalidades;
            }
        }
        public bool InsertarEmpresaTransporte(string descripcion, string direccion)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_EmpresaTransporte", conexion);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@direccion", direccion);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                    rpta = true;
                }
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return rpta;
            }
            return rpta;
        }
        public bool ActualizarEmpresaTransporte(int idEmpresaTranspte, string descripcion, string direccion)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_EmpresaTransporte", conexion);
                    cmd.Parameters.AddWithValue("@idEmpresaTranspte", idEmpresaTranspte);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    rpta = true;
                }
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return rpta;
            }
            return rpta;
        }
        public bool EliminarEmpresaTransporte(int idEmpresaTranspte)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_EmpresaTransporte", conexion);
                    cmd.Parameters.AddWithValue("@idEmpresaTranspte", idEmpresaTranspte);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    rpta = true;
                }
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return rpta;
            }
            return rpta;
        }
        public List<EmpresaTransporteModel> ObtenerEmpresaTransporte()
        {
            var oList = new List<EmpresaTransporteModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_EmpresaTransporte", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new EmpresaTransporteModel()
                            {
                                idEmpresaTranspte = Convert.ToInt32(dr["idEmpresaTranspte"]),
                                descripcion = dr["descripcion"].ToString(),
                                direccion = dr["direccion"].ToString(),

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

    }
}
