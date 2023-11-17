using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Sales.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Sales
{
    public class SP_Ventas : ISP_Ventas
    {
        public List<ComboBox> TipoVentaCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_TipoVenta_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idTipoVenta"]),
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
        public List<ProductoVenta> ObtenerProductosVenta()
        {
            var productosList = new List<ProductoVenta>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Productos_Venta", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            productosList.Add(new ProductoVenta()
                            {
                                IdProducto = Convert.ToInt32(dr["idProducto"]),
                                Producto = dr["producto"].ToString(),
                                Cantidad = Convert.ToInt32(dr["cantidad"]),
                                Marca = dr["marca"].ToString(),
                                Precio = Convert.ToDecimal(dr["precio"]),
                                ImagenCarpeta = dr["imagenCarpeta"].ToString(),
                                ImagenNombre = dr["imagenNombre"].ToString(),
                                IdSize = Convert.ToInt32(dr["idSize"]),
                                SizeDescription = dr["SizeDescription"].ToString(),
                                IdColor = Convert.ToInt32(dr["idColor"]),
                                ColorDescription = dr["ColorDescription"].ToString(),
                                IdModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                ModeloDescripcion = dr["ModeloDescripcion"].ToString(),
                                IdCalidad = Convert.ToInt32(dr["idCalidad"]),
                                CalidadDescripcion = dr["CalidadDescripcion"].ToString(),
                                IdMarca = Convert.ToInt32(dr["idMarca"]),
                                MarcaDescripcion = dr["MarcaDescripcion"].ToString(),
                                IdCategoria = Convert.ToInt32(dr["idMarca"]),
                                CategoriaDescripcion = dr["CategoriaDescripcion"].ToString(),
                            });
                        }
                    }
                }
                return productosList;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return productosList;
            }
        }
    }
}
