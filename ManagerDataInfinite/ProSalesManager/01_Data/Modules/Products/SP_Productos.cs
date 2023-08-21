
using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ReservedModels;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.Products
{
    public class SP_Productos : ISP_Productos
    {
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
        public List<SizeModel> SizesLista(int idProducto)
        {
            var oList = new List<SizeModel>();
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_AllSizes", conexion);
                    cmd.Parameters.AddWithValue("@intIdProducto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new SizeModel()
                            {
                                idSize = Convert.ToInt32(dr["idSize"]),
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

        public bool UpdateProducto(ProductoModel oProductoModel)
        {
            bool rpta = false;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Update_Producto", conexion);
                    cmd.Parameters.AddWithValue("@idProducto", oProductoModel.idProducto);
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
                    cmd.Parameters.AddWithValue("@idSize", oSizeModel.idSize);
                    cmd.Parameters.AddWithValue("@idProducto", oSizeModel.idProducto);
                    cmd.Parameters.AddWithValue("@descripcion", oSizeModel.descripcion);
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
                    cmd.Parameters.AddWithValue("@idProducto", oSizeModel.idProducto);
                    cmd.Parameters.AddWithValue("@descripcion", oSizeModel.descripcion);
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
