
using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._03_Models.ReservedModels;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Products
{
    public class SP_Productos : ISP_Productos
    {
        // Public Page
        public ProductoModel ProductoUnico(bool Activo, int idProducto)
        {
            ProductoModel producto = null; // Inicializamos como null para el caso de que no haya registros
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Productos", conexion);
                    cmd.Parameters.AddWithValue("@btActivo", Activo);
                    cmd.Parameters.AddWithValue("@intIdProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read()) // Usamos if en lugar de while ya que esperamos un solo registro
                        {
                            producto = new ProductoModel()
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
                                Images = dr["Images"].ToString().Split(',').ToList(),
                                Sizes = dr["Sizes"].ToString().Split(',').ToList(),
                                Tags = dr["Tags"].ToString().Split(',').ToList(),
                                createdAt = Convert.ToDateTime(dr["horaCreacion"]),
                                horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Genero = dr["Genero"].ToString(),
                                Colores = dr["Colores"].ToString().Split(',').ToList(),
                                NewLabelContent = dr["NewLabelContent"].ToString(),
                                NewLabelEnabled = Convert.ToBoolean(dr["NewLabelEnabled"]),
                                SaleLabelContent = dr["SaleLabelContent"].ToString(),
                                SaleLabelEnabled = Convert.ToBoolean(dr["SaleLabelEnabled"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                // Aquí podrías manejar el error como consideres más apropiado
                // Por ejemplo, podrías loguear el error o asignar un valor específico a 'producto'
            }
            return producto; // Devolvemos el producto, que será null si no se encontró ningún registro
        }

        //CrudModeloProducto
        public List<ModeloProductoModel> ModeloProductosListaCrud()
        {
            var oList = new List<ModeloProductoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_ModeloProducto", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ModeloProductoModel()
                            {
                                idModeloProductoDetalle = Convert.ToInt32(dr["idModeloProductoDetalle"]),
                                idModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                desModelo = dr["desModelo"].ToString(),
                                idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                desCategoria = dr["desCategoria"].ToString(),


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
        public List<ModeloProductoModel> ModeloProductosByIDCrud(int idModeloProducto)
        {
            var oList = new List<ModeloProductoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_ModeloProducto_byId", conexion);
                    cmd.Parameters.AddWithValue("@inIdModeloProducto", idModeloProducto);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ModeloProductoModel()
                            {
                                idModeloProductoDetalle = Convert.ToInt32(dr["idModeloProductoDetalle"]),
                                idModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                desModelo = dr["desModelo"].ToString(),
                                idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                desCategoria = dr["desCategoria"].ToString(),

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
        public bool InsertModeloProducto(ModeloProductoModel oModeloProductoModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_ModeloProducto", conexion);
                    cmd.Parameters.AddWithValue("@vchDescripcion", oModeloProductoModel.desModelo);
                    cmd.Parameters.AddWithValue("@inIdCategoria", oModeloProductoModel.idCategoria);


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
        public bool UpdateModeloProducto(ModeloProductoModel oModeloProductoModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_ModeloProducto", conexion);
                    cmd.Parameters.AddWithValue("@vchDescripcion", oModeloProductoModel.desModelo);
                    cmd.Parameters.AddWithValue("@inIdModeloProducto", oModeloProductoModel.idModeloProducto);
                    cmd.Parameters.AddWithValue("@inIdCategoria", oModeloProductoModel.idCategoria);


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
        public bool DeleteModeloProducto(int idModeloProducto)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_ModeloProducto", conexion);
                    cmd.Parameters.AddWithValue("@inIdModeloProducto", idModeloProducto);
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

        public List<ModeloProductoDetalleModel> ModeloProductosDetalleLista()
        {
            var oList = new List<ModeloProductoDetalleModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_ModeloProductoDetalle", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ModeloProductoDetalleModel()
                            {
                                idModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                idCategoria = Convert.ToInt32(dr["idCategoria"]),

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


        // Crud Page
        // Producto
        public List<CrudProductoModel> ProductosListaCrud()
        {
            var oList = new List<CrudProductoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Crud_Select_Productos", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CrudProductoModel()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                producto = dr["producto"].ToString(),
                                calidad = dr["calidad"].ToString(),
                                marca = dr["marca"].ToString(),
                                precio = Convert.ToDecimal(dr["precio"]),
                                cantidad = Convert.ToInt32(dr["cantidad"]),
                                idProveedor = Convert.ToInt32(dr["idProveedor"]),
                                fechaIngreso = Convert.ToDateTime(dr["fechaIngreso"]),
                                idEmpresa = Convert.ToInt32(dr["idEmpresa"]),
                                imagenCarpeta = dr["imagenCarpeta"].ToString(),
                                imagenNombre = dr["imagenNombre"].ToString(),
                                horaCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                idGenero = Convert.ToInt32(dr["idGenero"]),
                                idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                idModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                idCalidad = Convert.ToInt32(dr["idCalidad"]),
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
        public List<CrudProductoModel> ProductosByIDCrud(int idProducto)
        {
            var oList = new List<CrudProductoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Productos_byId", conexion);
                    cmd.Parameters.AddWithValue("@idProducto", idProducto);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CrudProductoModel()
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                producto = dr["producto"].ToString(),
                                marca = dr["marca"].ToString(),
                                costo = Convert.ToDecimal(dr["costo"]),
                                precio = Convert.ToDecimal(dr["precio"]),
                                cantidad = Convert.ToInt32(dr["cantidad"]),
                                idProveedor = Convert.ToInt32(dr["idProveedor"]),
                                fechaIngreso = Convert.ToDateTime(dr["fechaIngreso"]),
                                idEmpresa = Convert.ToInt32(dr["idEmpresa"]),
                                imagenCarpeta = dr["imagenCarpeta"].ToString(),
                                imagenNombre = dr["imagenNombre"].ToString(),
                                horaCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                idGenero = Convert.ToInt32(dr["idGenero"]),
                                idCategoria = Convert.ToInt32(dr["idCategoria"]),
                                idModeloProducto = Convert.ToInt32(dr["idModeloProducto"]),
                                idCalidad = Convert.ToInt32(dr["idCalidad"]),
                                idMarca = Convert.ToInt32(dr["idMarca"]),
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
        public List<ComboBox> ProveedorCrudCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Proveedores_ComboBox", conexion);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idProveedor"]),
                                descripcion = dr["proveedor"].ToString(),

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
        public List<ComboBox> GeneroCrudCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Genero_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idGenero"]),
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
        public List<ComboBox> CategoriaCrudCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Categoria_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idCategoria"]),
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
        public List<ComboBox> ModeloCrudCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_ModeloProducto_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idModeloProducto"]),
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

        public int UpdateProducto(CrudProductoModel oCrudProductoModel)
        {
            int returnedId = -1; // -1 indica que algo salió mal.
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Productos", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", oCrudProductoModel.idProducto);
                    cmd.Parameters.AddWithValue("@vcProducto", oCrudProductoModel.producto);
                    cmd.Parameters.AddWithValue("@dcPrecio", oCrudProductoModel.precio);
                    cmd.Parameters.AddWithValue("@inCantidad", oCrudProductoModel.cantidad);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oCrudProductoModel.idProveedor);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", 1);
                    cmd.Parameters.AddWithValue("@vcImagenCarpeta", oCrudProductoModel.imagenCarpeta);
                    cmd.Parameters.AddWithValue("@vcImagenNombre", oCrudProductoModel.imagenNombre);
                    cmd.Parameters.AddWithValue("@btActivo", oCrudProductoModel.Activo);
                    cmd.Parameters.AddWithValue("@inIdGenero", oCrudProductoModel.idGenero);
                    cmd.Parameters.AddWithValue("@inIdCategoria", oCrudProductoModel.idCategoria);
                    cmd.Parameters.AddWithValue("@inIdCalidad", oCrudProductoModel.idCalidad);
                    cmd.Parameters.AddWithValue("@inIdMarca", oCrudProductoModel.idMarca);
                    cmd.Parameters.AddWithValue("@inIdModeloProducto", oCrudProductoModel.idModeloProducto);
                    cmd.Parameters.AddWithValue("@dcCosto", oCrudProductoModel.costo);

                    SqlParameter outIdProductoParam = new SqlParameter("@outIdProducto", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outIdProductoParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery(); // Ejecuta el procedimiento almacenado

                    // Recuperar el valor OUTPUT
                    if (outIdProductoParam.Value != null)
                    {
                        returnedId = Convert.ToInt32(outIdProductoParam.Value);
                    }

                    conexion.Close();  // Cerrar la conexión explícitamente
                }
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return returnedId; // Retornará -1 en caso de error.
            }
            return returnedId; // Retorna el idProducto.
        }

        public bool DeleteProducto(int idProducto)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Producto", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", idProducto);
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

        //Size
        public List<CrudSizeDetalleModel> SizeDetalleByIDCrud(int idProducto)
        {
            var oList = new List<CrudSizeDetalleModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_SizeDetalle_byId", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", idProducto);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CrudSizeDetalleModel()
                            {
                                idSizeDetalle = Convert.ToInt32(dr["idSizeDetalle"]),
                                idSize = Convert.ToInt32(dr["idSize"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                descripcion = dr["descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),

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
        public List<ComboBox> SizeCrudCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Size_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idSize"]),
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
        public bool InsertSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_SizeDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdSize", oSizeDetalleModel.idSize);
                    cmd.Parameters.AddWithValue("@inIdProducto", oSizeDetalleModel.idProducto);
                    cmd.Parameters.AddWithValue("@btActivo", oSizeDetalleModel.Activo);

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
        public bool UpdateSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_SizeDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdSizeDetalle", oSizeDetalleModel.idSizeDetalle);
                    cmd.Parameters.AddWithValue("@inIdSize", oSizeDetalleModel.idSize);
                    cmd.Parameters.AddWithValue("@btActivo", oSizeDetalleModel.Activo);

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
        public bool DeleteSizeDetalle(int idSizeDetalle)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_SizeDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdSizeDetalle", idSizeDetalle);
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

        //Tags
        public List<CrudTagDetalleModel> TagsByIDCrud(int idProducto)
        {
            var oList = new List<CrudTagDetalleModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Tag_byId", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", idProducto);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CrudTagDetalleModel()
                            {
                                idTags = Convert.ToInt32(dr["idTags"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                descripcion = dr["descripcion"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),

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
        public bool InsertTagCrud(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Tag", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", oCrudTagDetalleModel.idProducto);
                    cmd.Parameters.AddWithValue("@vchDescripcion", oCrudTagDetalleModel.descripcion);
                    cmd.Parameters.AddWithValue("@btActivo", oCrudTagDetalleModel.Activo);

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
        public bool UpdateTagCrud(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Tag", conexion);
                    cmd.Parameters.AddWithValue("@inIdTags", oCrudTagDetalleModel.idTags);
                    cmd.Parameters.AddWithValue("@inIdProducto", oCrudTagDetalleModel.idProducto);
                    cmd.Parameters.AddWithValue("@vchDscripcion", oCrudTagDetalleModel.descripcion);
                    cmd.Parameters.AddWithValue("@btActivo", oCrudTagDetalleModel.Activo);

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
        public bool DeleteTagCrud(int id)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Tag", conexion);
                    cmd.Parameters.AddWithValue("@inIdTags", id);
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

        //Color
        public List<ComboBox> ColorDetalleCrudCB()
        {
            var oList = new List<ComboBox>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Color_ComboBox", conexion);

                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ComboBox()
                            {
                                id = Convert.ToInt32(dr["idColor"]),
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
        public List<CrudColorDetalleModel> ColorDetalleByIDCrud(int idProducto)
        {
            var oList = new List<CrudColorDetalleModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_ColorDetalle_byId", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", idProducto);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CrudColorDetalleModel()
                            {
                                idColorDetalle = Convert.ToInt32(dr["idColorDetalle"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                idColor = Convert.ToInt32(dr["idColor"]),
                                codigo = dr["codigo"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),

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
        public bool InsertColorDetalleCrud(CrudColorDetalleModel crudColorDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_ColorDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", crudColorDetalleModel.idProducto);
                    cmd.Parameters.AddWithValue("@inIdColor", crudColorDetalleModel.idColor);
                    cmd.Parameters.AddWithValue("@bActivo", crudColorDetalleModel.Activo);

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
        public bool UpdateColorDetalleCrud(CrudColorDetalleModel crudColorDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_ColorDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdColor", crudColorDetalleModel.idColor);
                    cmd.Parameters.AddWithValue("@bActivo", crudColorDetalleModel.Activo);
                    cmd.Parameters.AddWithValue("@inIdColorDetalle", crudColorDetalleModel.idColorDetalle);


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
        public bool DeleteColorDetalleCrud(int id)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_ColorDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdColorDetalle", id);
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

        //imagenes
        public List<CrudImagenModel> ImagenByIDCrud(int idProducto)
        {
            var oList = new List<CrudImagenModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Imagenes_byId", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", idProducto);

                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new CrudImagenModel()
                            {
                                idImagenes = Convert.ToInt32(dr["idImagenes"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                rutaImagen = dr["rutaImagen"].ToString(),
                                nombreImagen = dr["nombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"]),

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
        public bool InsertImagenes(ImagenModel oImagenModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Imagen", conexion);
                    cmd.Parameters.AddWithValue("@idProducto", oImagenModel.idProducto);
                    cmd.Parameters.AddWithValue("@rutaImagen", oImagenModel.rutaImagen);
                    cmd.Parameters.AddWithValue("@nombreImagen", oImagenModel.nombreImagen);
                    cmd.Parameters.AddWithValue("@Activo", 1);

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
        public bool DeleteImagenes(int idImagenes)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Imagen", conexion);
                    cmd.Parameters.AddWithValue("@idImagenes", idImagenes);

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
        //ColorCrud
        public List<ColorModel> GetAllColors()
        {
            var oList = new List<ColorModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Color", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ColorModel()
                            {
                                IdColor = Convert.ToInt32(dr["idColor"]),
                                Codigo = dr["codigo"].ToString(),
                                Descripcion = dr["descripcion"].ToString(),
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

        public bool InsertColor(ColorModel oColorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Color", conexion);
                    cmd.Parameters.AddWithValue("@codigo", oColorModel.Codigo);
                    cmd.Parameters.AddWithValue("@descripcion", oColorModel.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oColorModel.Activo);

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

        public bool UpdateColor(ColorModel oColorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Color", conexion);
                    cmd.Parameters.AddWithValue("@idColor", oColorModel.IdColor);
                    cmd.Parameters.AddWithValue("@codigo", oColorModel.Codigo);
                    cmd.Parameters.AddWithValue("@descripcion", oColorModel.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oColorModel.Activo);

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

        public bool DeleteColor(int idColor)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Color", conexion);
                    cmd.Parameters.AddWithValue("@idColor", idColor);

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

        //SizeCrud
        public List<SizeModel> GetAllSizes()
        {
            var oList = new List<SizeModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Size", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new SizeModel()
                            {
                                IdSize = Convert.ToInt32(dr["idSize"]),
                                Descripcion = dr["descripcion"].ToString(),
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

        public bool InsertSize(SizeModel oSizeModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Size", conexion);
                    cmd.Parameters.AddWithValue("@descripcion", oSizeModel.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oSizeModel.Activo);

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

        public bool UpdateSize(SizeModel oSizeModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Size", conexion);
                    cmd.Parameters.AddWithValue("@idSize", oSizeModel.IdSize);
                    cmd.Parameters.AddWithValue("@descripcion", oSizeModel.Descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oSizeModel.Activo);

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

        public bool DeleteSize(int idSize)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Size", conexion);
                    cmd.Parameters.AddWithValue("@idSize", idSize);

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


        //ProveedorCrud
        public List<ProveedoresModel> GetAllProveedores()
        {
            var oList = new List<ProveedoresModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Proveedores", conexion);
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
                                horaActualizacion= Convert.ToDateTime(dr["horaActualizacion"]),
                                idEmpresa= Convert.ToInt32(dr["idEmpresa"]),
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

        public bool InsertProveedor(ProveedoresModel oProveedorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@vcProveedor", oProveedorModel.proveedor);
                    cmd.Parameters.AddWithValue("@vcContacto", oProveedorModel.contacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oProveedorModel.telefono);
                    cmd.Parameters.AddWithValue("@vcDireccion", oProveedorModel.direccion);
                    cmd.Parameters.AddWithValue("@dtFecha", oProveedorModel.fecha);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProveedorModel.idEmpresa);

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

        public bool UpdateProveedor(ProveedoresModel oProveedorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oProveedorModel.idProveedor);
                    cmd.Parameters.AddWithValue("@vcProveedor", oProveedorModel.proveedor);
                    cmd.Parameters.AddWithValue("@vcContacto", oProveedorModel.contacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oProveedorModel.telefono);
                    cmd.Parameters.AddWithValue("@vcDireccion", oProveedorModel.direccion);
                    cmd.Parameters.AddWithValue("@dtFecha", oProveedorModel.fecha);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProveedorModel.idEmpresa);

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

        public bool DeleteProveedor(int idProveedor)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@inIdProveedor", idProveedor);

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


        // No actualizado
        public List<ImagenModel> ImagenesLista(int idProducto)
        {
            var oList = new List<ImagenModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_AllImagenes", conexion);
                    cmd.Parameters.AddWithValue("@intIdProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ImagenModel()
                            {
                                idImagenes = Convert.ToInt32(dr["idImagenes"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                rutaImagen = dr["rutaImagen"].ToString(),
                                nombreImagen = dr["nombreImagen"].ToString(),
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
        public List<TagModel> TagsLista(int idProducto)
        {
            var oList = new List<TagModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_AllTags", conexion);
                    cmd.Parameters.AddWithValue("@intIdProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new TagModel()
                            {
                                idTags = Convert.ToInt32(dr["idTags"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                descripcion = dr["descripcion"].ToString(),
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
        //public List<SizeModel> SizesLista(int idProducto)
        //{
        //    var oList = new List<SizeModel>();
        //    try
        //    {
        //        var cn = new DataConnection();
        //        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //        {
        //            conexion.Open();
        //            SqlCommand cmd = new SqlCommand("SP_Select_AllSizes", conexion);
        //            cmd.Parameters.AddWithValue("@intIdProducto", idProducto);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            using (var dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    oList.Add(new SizeModel()
        //                    {
        //                        idSize = Convert.ToInt32(dr["idSize"]),
        //                        idProducto = Convert.ToInt32(dr["idProducto"]),
        //                        descripcion = dr["descripcion"].ToString(),
        //                        Activo = Convert.ToBoolean(dr["Activo"])
        //                    });
        //                }
        //            }
        //        }
        //        return oList;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResult.ErrorMessage = ex.Message;
        //        return oList;
        //    }
        //}
        public List<DescripcionModel> DescripcionesLista(int idProducto)
        {
            var oList = new List<DescripcionModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_AllDescripciones", conexion);
                    cmd.Parameters.AddWithValue("@intIdProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new DescripcionModel()
                            {
                                idDescripcion = Convert.ToInt32(dr["idDescripcion"]),
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                descripcion = dr["descripcion"].ToString(),
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

        
        public bool UpdateImagenes(ImagenModel oImagenModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Imagenes", conexion);
                    cmd.Parameters.AddWithValue("@idImagenes", oImagenModel.idImagenes);
                    cmd.Parameters.AddWithValue("@idProducto", oImagenModel.idProducto);
                    cmd.Parameters.AddWithValue("@rutaImagen", oImagenModel.rutaImagen);
                    cmd.Parameters.AddWithValue("@nombreImagen", oImagenModel.nombreImagen);
                    cmd.Parameters.AddWithValue("@Activo", oImagenModel.Activo);

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
        public bool UpdateTag(TagModel oTagModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Tag", conexion);
                    cmd.Parameters.AddWithValue("@idTags", oTagModel.idTags);
                    cmd.Parameters.AddWithValue("@idProducto", oTagModel.idProducto);
                    cmd.Parameters.AddWithValue("@descripcion", oTagModel.descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oTagModel.Activo);

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
        //public bool UpdateSize(SizeModel oSizeModel)
        //{
        //    bool rpta = false;
        //    try
        //    {
        //        var cn = new DataConnection();
        //        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //        {
        //            conexion.Open();
        //            SqlCommand cmd = new SqlCommand("SP_Update_Size", conexion);
        //            cmd.Parameters.AddWithValue("@idSize", oSizeModel.idSize);
        //            cmd.Parameters.AddWithValue("@idProducto", oSizeModel.idProducto);
        //            cmd.Parameters.AddWithValue("@descripcion", oSizeModel.descripcion);
        //            cmd.Parameters.AddWithValue("@Activo", oSizeModel.Activo);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //        }
        //        rpta = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResult.ErrorMessage = ex.Message;
        //        return rpta;
        //    }
        //    return rpta;
        //}
        public bool UpdateDescripcion(DescripcionModel oDescripcionModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Descripcion", conexion);
                    cmd.Parameters.AddWithValue("@idDescripcion", oDescripcionModel.idDescripcion);
                    cmd.Parameters.AddWithValue("@idProducto", oDescripcionModel.idProducto);
                    cmd.Parameters.AddWithValue("@descripcion", oDescripcionModel.descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oDescripcionModel.Activo);

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
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Producto", conexion);
                    cmd.Parameters.AddWithValue("@producto", oProductoModel.producto);
                    cmd.Parameters.AddWithValue("@marca", oProductoModel.marca);
                    cmd.Parameters.AddWithValue("@precio", oProductoModel.precio);
                    cmd.Parameters.AddWithValue("@cantidad", oProductoModel.cantidad);
                    cmd.Parameters.AddWithValue("@idProveedor", oProductoModel.idProveedor);
                    cmd.Parameters.AddWithValue("@fechaIngreso", oProductoModel.fechaIngreso);
                    cmd.Parameters.AddWithValue("@idEmpresa", oProductoModel.idEmpresa);
                    cmd.Parameters.AddWithValue("@imagenCarpeta", oProductoModel.imagenCarpeta);
                    cmd.Parameters.AddWithValue("@imagenNombre", oProductoModel.imagenNombre);
                    cmd.Parameters.AddWithValue("@Activo", oProductoModel.Activo);

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

        public bool InsertTag(TagModel oTagModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Tag", conexion);
                    cmd.Parameters.AddWithValue("@idProducto", oTagModel.idProducto);
                    cmd.Parameters.AddWithValue("@descripcion", oTagModel.descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oTagModel.Activo);

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
        //public bool InsertSize(SizeModel oSizeModel)
        //{
        //    bool rpta = false;
        //    try
        //    {
        //        var cn = new DataConnection();
        //        using (var conexion = new SqlConnection(cn.getCadenaSQL()))
        //        {
        //            conexion.Open();
        //            SqlCommand cmd = new SqlCommand("SP_Insert_Size", conexion);
        //            cmd.Parameters.AddWithValue("@idProducto", oSizeModel.idProducto);
        //            cmd.Parameters.AddWithValue("@descripcion", oSizeModel.descripcion);
        //            cmd.Parameters.AddWithValue("@Activo", oSizeModel.Activo);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.ExecuteNonQuery();
        //        }
        //        rpta = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResult.ErrorMessage = ex.Message;
        //        return rpta;
        //    }
        //    return rpta;
        //}
        public bool InsertDescripcion(DescripcionModel oDescripcionModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Descripcion", conexion);
                    cmd.Parameters.AddWithValue("@idProducto", oDescripcionModel.idProducto);
                    cmd.Parameters.AddWithValue("@descripcion", oDescripcionModel.descripcion);
                    cmd.Parameters.AddWithValue("@Activo", oDescripcionModel.Activo);

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
