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
        public bool InsertarVentaConDetalles(Venta venta)
        {
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarVentaYDetalles", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros para la venta
                        cmd.Parameters.Add(new SqlParameter("@fechaVenta", venta.FechaVenta));
                        cmd.Parameters.Add(new SqlParameter("@idCliente", venta.IdCliente));
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", venta.IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idEmpresa", venta.IdEmpresa));
                        cmd.Parameters.Add(new SqlParameter("@idTipoPago", venta.IdTipoPago));
                        cmd.Parameters.Add(new SqlParameter("@TotalDefinido", (object)venta.TotalDefinido ?? DBNull.Value));

                        // Parámetro para los detalles de la venta
                        var detallesTabla = new DataTable();
                        detallesTabla.Columns.Add("idProducto", typeof(int));
                        detallesTabla.Columns.Add("cantidad", typeof(int));
                        detallesTabla.Columns.Add("precioUnitario", typeof(decimal));

                        foreach (var detalle in venta.DetallesVenta)
                        {
                            detallesTabla.Rows.Add(detalle.IdProducto, detalle.Cantidad, detalle.PrecioUnitario);
                        }

                        var param = cmd.Parameters.AddWithValue("@detallesVenta", detallesTabla);
                        param.SqlDbType = SqlDbType.Structured;
                        param.TypeName = "dbo.TipoDetalleVenta"; // Especifica el nombre del tipo de tabla en SQL Server

                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente
                ErrorResult.ErrorMessage = ex.Message;
                return false;
            }
        }
        public List<VentasModel> ObtenerVentas()
        {
            var oList = new List<VentasModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Ventas", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new VentasModel()
                            {
                                idVenta = Convert.ToInt32(dr["idVenta"]),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                nombres = dr["nombres"].ToString(),
                                idTipoPago = Convert.ToInt32(dr["idTipoPago"]),
                                tipoPago = dr["tipoPago"].ToString(),
                                idEmpresa = Convert.ToInt32(dr["idEmpresa"]),
                                total = Convert.ToDecimal(dr["total"]),
                                fechaVenta = dr["fechaVenta"].ToString(),
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
