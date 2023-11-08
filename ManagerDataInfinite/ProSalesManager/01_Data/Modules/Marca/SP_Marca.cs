using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Marca.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Marca
{
    public class SP_Marca :ISP_Marca
    {
        public List<MarcaModel> ObtenerMarca()
        {
            var oList = new List<MarcaModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Marca", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new MarcaModel()
                            {
                                idMarca = Convert.ToInt32(dr["idMarca"]),
                                descripcion = dr["descripcion"].ToString(),

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

        public List<ComboBox> ObtenerMarcasParaComboBox()
        {
            var listaCalidades = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Marca_Combobox", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaCalidades.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idMarca"]),
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

        public bool InsertarMarca(string descripcion)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Marca", conexion);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
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

        public bool ActualizarMarca(int idMarca, string descripcion)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Marca", conexion);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
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

        public bool EliminarMarca(int idMarca)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Marca", conexion);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
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
    }
}
