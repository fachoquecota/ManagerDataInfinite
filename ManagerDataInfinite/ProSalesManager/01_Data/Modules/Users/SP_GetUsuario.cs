using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Users.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Users
{
    public class SP_GetUsuario : ISP_Usuario
    {
        public UsuarioModel GetUsuario(string correo)
        {
            var usuario = new UsuarioModel();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Usuarios", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@vcCorreo", correo);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario.Nombre = dr["nombre"].ToString();
                            usuario.Apellido = dr["apellido"].ToString();
                            usuario.Correo = dr["correo"].ToString();
                            usuario.Usuario = dr["usuario"].ToString();
                            usuario.IdEmpresa = Convert.ToInt32(dr["IdEmpresa"]);
                        }
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return usuario;
            }
        }
    }
}
