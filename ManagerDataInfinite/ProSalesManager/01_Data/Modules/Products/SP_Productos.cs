
using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Products
{
    public class SP_Productos : ISP_Productos
    {
        public List<ProductoModel> ProductosLista(string usuarioNavegacion)
        {
            //* Utilizamos DBBoolResult de tipo Models para recepcionar la información porque
            //* cabe la posibilidad de que se aumenten más columnas en la respuesta del procedimiento almacenado
            //* es posible usar otro método para aumentar las columnas sin necesida de cambiar este código
            var oList = new List<ProductoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Productos", conexion);
                    cmd.Parameters.AddWithValue("@vcCorreo", usuarioNavegacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ProductoModel()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                
                                producto = dr["producto"].ToString(),
                                marca = dr["marca"].ToString(),
                                precio = Convert.ToDouble(dr["precio"]),
                                cantidad = Convert.ToInt32(dr["cantidad"]),
                                idProveedor = Convert.ToInt32(dr["idProveedor"]),
                                fechaIngreso = Convert.ToDateTime(dr["fechaIngreso"]),
                                imagenCarpeta = dr["imagenCarpeta"].ToString(),
                                imagenNombre = dr["imagenNombre"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
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
        public bool UpdateProducto(ProductoModel oProductoModel)
        {
            bool rpta=false;
            try
            {
                var cn = new DataConnection();
                DateTime datetime = DateTime.Now;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Productos", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", oProductoModel.idProducto);
                    cmd.Parameters.AddWithValue("@vcProducto", oProductoModel.producto);
                    cmd.Parameters.AddWithValue("@vcMarca", oProductoModel.marca);
                    cmd.Parameters.AddWithValue("@dcPrecio", oProductoModel.precio);
                    cmd.Parameters.AddWithValue("@inCantidad", oProductoModel.cantidad);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oProductoModel.idProveedor);
                    cmd.Parameters.AddWithValue("@dtFechaIngreso", oProductoModel.fechaIngreso);
                    cmd.Parameters.AddWithValue("@vcImagenCarpeta", oProductoModel.imagenCarpeta);
                    cmd.Parameters.AddWithValue("@vcImagenNombre", oProductoModel.imagenNombre);
                    cmd.Parameters.AddWithValue("@btActivo", oProductoModel.Activo);

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
        public bool InsertProducto(ProductoModel oProductoModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                DateTime datetime = DateTime.Now;
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Productos", conexion);
                    cmd.Parameters.AddWithValue("@vcProducto", oProductoModel.producto);
                    cmd.Parameters.AddWithValue("@vcMarca", oProductoModel.marca);
                    cmd.Parameters.AddWithValue("@dcPrecio", oProductoModel.precio);
                    cmd.Parameters.AddWithValue("@inCantidad", oProductoModel.cantidad);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oProductoModel.idProveedor);
                    cmd.Parameters.AddWithValue("@dtFechaIngreso", oProductoModel.fechaIngreso);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProductoModel.idEmpresa);
                    cmd.Parameters.AddWithValue("@vcImagenCarpeta", oProductoModel.imagenCarpeta);
                    cmd.Parameters.AddWithValue("@vcImagenNombre", oProductoModel.imagenNombre);
                    cmd.Parameters.AddWithValue("@btActivo", oProductoModel.Activo);

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
