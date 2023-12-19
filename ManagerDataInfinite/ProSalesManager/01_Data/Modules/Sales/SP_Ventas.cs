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
                        cmd.Parameters.Add(new SqlParameter("@idTransporteCombobox", venta.idTransporteCombobox));

                        cmd.Parameters.Add(new SqlParameter("@TotalDefinido", (object)venta.TotalDefinido ?? DBNull.Value));

                        cmd.Parameters.Add(new SqlParameter("@departamento", venta.Ubicacion.Departamento));
                        cmd.Parameters.Add(new SqlParameter("@provincia", venta.Ubicacion.Provincia));
                        cmd.Parameters.Add(new SqlParameter("@distrito", venta.Ubicacion.Distrito));

                        // Parámetro para los detalles de la venta
                        var detallesTabla = new DataTable();
                        detallesTabla.Columns.Add("idProducto", typeof(int));
                        detallesTabla.Columns.Add("cantidad", typeof(int));
                        detallesTabla.Columns.Add("precioUnitario", typeof(decimal));

                        foreach (var detalle in venta.DetallesVenta)
                        {
                            detallesTabla.Rows.Add(detalle.IdProducto, detalle.Cantidad, detalle.Precio);
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
                                idEmpresaTranspte = Convert.ToInt32(dr["idEmpresa"]),
                                total = Convert.ToDecimal(dr["total"]),
                                fechaVenta = dr["fechaVenta"].ToString(),
                                empresaTransporte = dr["empresaTransporte"].ToString(),
                                idUbigeo = Convert.ToInt32(dr["idUbigeo"]),
                                ubigeo = dr["ubigeo"].ToString(),



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
        public List<VentasModel> ObtenerVentasFiltro(FiltroVentasModel filtro)
        {
            var oList = new List<VentasModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Ventas_byFilter", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros al comando
                    cmd.Parameters.Add(new SqlParameter("@idCliente", filtro.IdCliente ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@idEmpresaTranspte", filtro.idEmpresaTranspte ?? (object)DBNull.Value));

                    cmd.Parameters.Add(new SqlParameter("@departamento", filtro.Departamento ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@provincia", filtro.Provincia ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@distrito", filtro.Distrito ?? (object)DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@horaInicio", filtro.FechaInicio.HasValue ? (object)filtro.FechaInicio.Value : DBNull.Value));
                    cmd.Parameters.Add(new SqlParameter("@horaFin", filtro.FechaFin.HasValue ? (object)filtro.FechaFin.Value : DBNull.Value));

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
                                idEmpresaTranspte = Convert.ToInt32(dr["idEmpresaTranspte"]),
                                total = Convert.ToDecimal(dr["total"]),
                                fechaVenta = dr["fechaVenta"].ToString(),
                                empresaTransporte = dr["empresaTransporte"].ToString(),
                                idUbigeo = Convert.ToInt32(dr["idUbigeo"]),
                                ubigeo = dr["ubigeo"].ToString(),
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
        public List<UbigeoModel> ObtenerUbigeoVenta()
        {
            var oList = new List<UbigeoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Ubigeo", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new UbigeoModel()
                            {
                                idUbigeo = Convert.ToInt32(dr["idUbigeo"]),
                                departamento = dr["departamento"].ToString(),
                                provincia = dr["provincia"].ToString(),
                                distrito = dr["distrito"].ToString(),


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
        public List<ReporteDetalle> ObtenerRptDetalle(int idVenta)
        {
            var oList = new List<ReporteDetalle>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Venta_Rpt_Detalle", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idVenta", idVenta));

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ReporteDetalle()
                            {
                                idVenta = Convert.ToInt32(dr["idVenta"]),
                                fechaVenta = dr["fechaVenta"].ToString(),
                                totalDefinido = dr["totalDefinido"].ToString(),
                                imagenCarpeta = dr["imagenCarpeta"].ToString(),
                                imagenNombre = dr["imagenNombre"].ToString(),
                                producto = dr["producto"].ToString(),
                                calidad = dr["calidad"].ToString(),
                                cantidad = dr["cantidad"].ToString(),
                                talla = dr["talla"].ToString(),
                                precioUnitario = dr["precioUnitario"].ToString(),
                                subtotal = dr["subtotal"].ToString(),
                                


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

        public List<ReporteVentaGraficoModel> ReporteVentaGrafico()
        {
            var oList = new List<ReporteVentaGraficoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Reporte_Venta", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ReporteVentaGraficoModel()
                            {
                                idVenta = Convert.ToInt32(dr["idVenta"]),
                                fechaVenta = Convert.ToDateTime(dr["fechaVenta"]),
                                idCliente = Convert.ToInt32(dr["idCliente"]),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                idUsuario = Convert.ToInt32(dr["idUsuario"]),
                                idEmpresa = Convert.ToInt32(dr["idEmpresa"]),
                                idTipoPago = Convert.ToInt32(dr["idTipoPago"]),
                                TipoPago = dr["TipoPago"].ToString(),
                                horaCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                TotalDefinido = dr["TotalDefinido"].ToString(),
                                idEmpresaTranspte = Convert.ToInt32(dr["idEmpresaTranspte"]),
                                NombreEmpresaTransporte = dr["NombreEmpresaTransporte"].ToString(),
                                idUbigeo = Convert.ToInt32(dr["idUbigeo"]),
                                NombreDepartamento = dr["NombreDepartamento"].ToString(),
                            });
                        }
                    }
                }
                return oList;
            }
            catch (Exception ex)
            {
                // Manejo del error (asegúrate de tener esta parte adecuadamente implementada)
                ErrorResult.ErrorMessage = ex.Message;
                return oList;
            }
        }
        public List<VentaDetalleModel> ReporteVentaDetalleGrafico()
        {
            var detallesList = new List<VentaDetalleModel>();
            try
            {
                var cn = new DataConnection(); // Asumiendo que DataConnection es una clase que provee la cadena de conexión
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Reporte_VentaDetalle", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            detallesList.Add(new VentaDetalleModel
                            {
                                IdVentaDetalle = Convert.ToInt32(dr["idVentaDetalle"]),
                                IdProducto = Convert.ToInt32(dr["idProducto"]),
                                Producto = dr["producto"].ToString(),
                                IdCategoria = Convert.ToInt32(dr["idCategoria"]),
                                NombreCategoria = dr["NombreCategoria"].ToString(),
                                IdModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                NombreModeloProducto = dr["NombreModeloProducto"].ToString(),
                                IdCalidad = Convert.ToInt32(dr["idCalidad"]),
                                NombreCalidad = dr["NombreCalidad"].ToString(),
                                IdMarca = Convert.ToInt32(dr["idMarca"]),
                                NombreMarca = dr["NombreMarca"].ToString(),
                                Cantidad = Convert.ToInt32(dr["cantidad"])
                            });
                        }
                    }
                }
                return detallesList;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return detallesList;
            }

        }


    }
}
