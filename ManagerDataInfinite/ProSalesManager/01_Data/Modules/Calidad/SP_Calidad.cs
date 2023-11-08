using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Calidad.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Calidad
{
    public class SP_Calidad : ISP_Calidad
    {

        public List<CalidadModel> ObtenerCalidad()
        {
            var oList = new List<CalidadModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Calidad", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CalidadModel()
                            {
                                idCalidad = Convert.ToInt32(dr["idCalidad"]),
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

        public List<ComboBox> ObtenerCalidadesParaComboBox()
        {
            var listaCalidades = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Calidad_Combobox", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            listaCalidades.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idCalidad"]),
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

        public bool InsertarCalidad(string descripcion)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Calidad", conexion);
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

        public bool ActualizarCalidad(int idCalidad, string descripcion)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Calidad", conexion);
                    cmd.Parameters.AddWithValue("@idCalidad", idCalidad);
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

        public bool EliminarCalidad(int idCalidad)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Calidad", conexion);
                    cmd.Parameters.AddWithValue("@idCalidad", idCalidad);
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
