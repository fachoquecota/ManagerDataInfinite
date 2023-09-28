﻿
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
        public List<ProductoModel> ProductosLista(bool Activo)
        {
            var oList = new List<ProductoModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Productos", conexion);
                    cmd.Parameters.AddWithValue("@btActivo", Activo);
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
                                Images = dr["Images"].ToString().Split(',').ToList(),
                                Sizes = dr["Sizes"].ToString().Split(',').ToList(),
                                Tags = dr["Tags"].ToString().Split(',').ToList(),
                                horaCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                Activo = Convert.ToBoolean(dr["Activo"]),
                                Genero = dr["Genero"].ToString(),
                                Colores = dr["Colores"].ToString().Split(',').ToList(),
                                NewLabelContent = dr["NewLabelContent"].ToString(),
                                NewLabelEnabled = Convert.ToBoolean(dr["NewLabelEnabled"]),
                                SaleLabelContent = dr["SaleLabelContent"].ToString(),
                                SaleLabelEnabled = Convert.ToBoolean(dr["SaleLabelEnabled"])
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
                                idCategoria = Convert.ToInt32(dr["idCategoria"])
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
                                idCategoria = Convert.ToInt32(dr["idCategoria"])
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

        public bool UpdateProducto(CrudProductoModel oCrudProductoModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Productos", conexion);
                    cmd.Parameters.AddWithValue("@inIdProducto", oCrudProductoModel.idProducto);
                    cmd.Parameters.AddWithValue("@vcProducto", oCrudProductoModel.producto);
                    cmd.Parameters.AddWithValue("@vcMarca", oCrudProductoModel.marca);
                    cmd.Parameters.AddWithValue("@dcPrecio", oCrudProductoModel.precio);
                    cmd.Parameters.AddWithValue("@inCantidad", oCrudProductoModel.cantidad);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oCrudProductoModel.idProveedor);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", 1);
                    cmd.Parameters.AddWithValue("@vcImagenCarpeta", oCrudProductoModel.imagenCarpeta);
                    cmd.Parameters.AddWithValue("@vcImagenNombre", oCrudProductoModel.imagenNombre);
                    cmd.Parameters.AddWithValue("@btActivo", oCrudProductoModel.Activo);
                    cmd.Parameters.AddWithValue("@inIdGenero", oCrudProductoModel.idGenero);
                    cmd.Parameters.AddWithValue("@inIdCategoria", oCrudProductoModel.idCategoria);

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
        public bool DeleteSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_SizeDetalle", conexion);
                    cmd.Parameters.AddWithValue("@inIdSizeDetalle", oSizeDetalleModel.idSizeDetalle);
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
        public bool DeleteTagCrud(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Tag", conexion);
                    cmd.Parameters.AddWithValue("@inIdTags", oCrudTagDetalleModel.idTags);
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

        public bool DeleteColor(ColorModel oColorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Color", conexion);
                    cmd.Parameters.AddWithValue("@idColor", oColorModel.IdColor);

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

        public bool DeleteSize(SizeModel oSizeModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Delete_Size", conexion);
                    cmd.Parameters.AddWithValue("@idSize", oSizeModel.IdSize);

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

        //ProvedoresCrud
        public List<ProveedorModel> GetProveedorByCorreo(string correo)
        {
            var oList = new List<ProveedorModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@vcCorreo", correo);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new ProveedorModel()
                            {
                                IdProveedor = Convert.ToInt32(dr["idProveedor"]),
                                Proveedor = dr["proveedor"].ToString(),
                                Contacto = dr["contacto"].ToString(),
                                Telefono = dr["telefono"].ToString(),
                                Direccion = dr["direccion"].ToString(),
                                Fecha = Convert.ToDateTime(dr["fecha"]),
                                HoraCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                HoraActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                IdEmpresa = Convert.ToInt32(dr["idEmpresa"])
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

        public bool InsertProveedor(ProveedorModel oProveedorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@vcProveedor", oProveedorModel.Proveedor);
                    cmd.Parameters.AddWithValue("@vcContacto", oProveedorModel.Contacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oProveedorModel.Telefono);
                    cmd.Parameters.AddWithValue("@vcDireccion", oProveedorModel.Direccion);
                    cmd.Parameters.AddWithValue("@dtFecha", oProveedorModel.Fecha);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProveedorModel.IdEmpresa);

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

        public bool UpdateProveedor(ProveedorModel oProveedorModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Proveedores", conexion);
                    cmd.Parameters.AddWithValue("@inIdProveedor", oProveedorModel.IdProveedor);
                    cmd.Parameters.AddWithValue("@vcProveedor", oProveedorModel.Proveedor);
                    cmd.Parameters.AddWithValue("@vcContacto", oProveedorModel.Contacto);
                    cmd.Parameters.AddWithValue("@vcTelefono", oProveedorModel.Telefono);
                    cmd.Parameters.AddWithValue("@vcDireccion", oProveedorModel.Direccion);
                    cmd.Parameters.AddWithValue("@dtFecha", oProveedorModel.Fecha);
                    cmd.Parameters.AddWithValue("@inIdEmpresa", oProveedorModel.IdEmpresa);

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
        public bool InsertImagenes(ImagenModel oImagenModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Insert_Imagenes", conexion);
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
