using ProSalesManager._01_Data.Connection;
using ProSalesManager._01_Data.Modules.PublicProducts.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.PublicProducts;
using ProSalesManager._03_Models.ReservedModels;
using System.Data;
using System.Data.SqlClient;

namespace ProSalesManager._01_Data.Modules.PublicProducts
{
    public class SP_PublicProducts : ISP_PublicProducts
    {
        public PublicProducsModel ObtenerProductoPorId(bool Activo, int idProducto)
        {
            PublicProducsModel producto = null;
            try
            {
                var cn = new DataConnection();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_Select_Producto_byId", conexion);
                    cmd.Parameters.AddWithValue("@btActivo", Activo);
                    cmd.Parameters.AddWithValue("@intIdProdcuto", idProducto);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        Dictionary<int, PublicSizeModel> sizes = new Dictionary<int, PublicSizeModel>();

                        while (dr.Read())
                        {
                            if (producto == null)
                            {
                                // La inicialización del producto principal solo se hace una vez.
                                producto = new PublicProducsModel
                                {
                                    idProducto = Convert.ToInt32(dr["idProducto"]),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    producto = dr["producto"].ToString(),
                                    marca = dr["marca"].ToString(),
                                    precio = Convert.ToDouble(dr["precio"]),
                                    cantidad = Convert.ToInt32(dr["cantidad"]),
                                    fechaIngreso = Convert.ToDateTime(dr["fechaIngreso"]),
                                    imagenCarpeta = dr["imagenCarpeta"].ToString(),
                                    imagenNombre = dr["imagenNombre"].ToString(),
                                    horaCreacion = Convert.ToDateTime(dr["horaCreacion"]),
                                    horaActualizacion = Convert.ToDateTime(dr["horaActualizacion"]),
                                    idProveedor = Convert.ToInt32(dr["idProveedor"]),
                                    proveedor = dr["proveedor"].ToString(),
                                    Categoria = dr["Categoria"].ToString(),
                                    Images = dr["Images"].ToString().Split(',').ToList(),
                                    descripcion = dr["descripcion"].ToString(),
                                    Genero = dr["Genero"].ToString(),
                                    NewLabelContent = dr["NewLabelContent"].ToString(),
                                    NewLabelEnabled = Convert.ToBoolean(dr["NewLabelEnabled"]),
                                    SaleLabelContent = dr["SaleLabelContent"].ToString(),
                                    SaleLabelEnabled = Convert.ToBoolean(dr["SaleLabelEnabled"]),
                                    Sizes = new List<PublicSizeModel>() // Inicializamos Sizes aquí pero lo llenaremos más adelante.
                                };
                            }


                            int currentSizeId = Convert.ToInt32(dr["idSize"]);
                            if (!sizes.ContainsKey(currentSizeId))
                            {
                                sizes[currentSizeId] = new PublicSizeModel
                                {
                                    IdSize = currentSizeId,
                                    Descripciones = dr["Sizes"].ToString().Split(',').ToList(),
                                    Colores = new List<PublicColorModel>()
                                };
                            }

                            sizes[currentSizeId].Colores.Add(new PublicColorModel
                            {
                                IdColor = Convert.ToInt32(dr["idColor"]),
                                DescripcionColor = dr["Colores"].ToString()
                            });
                        }

                        if (producto != null)
                        {
                            producto.Sizes = sizes.Values.ToList();
                        }
                    }
                }
                return producto;
            }
            catch (Exception ex)
            {
                ErrorResult.ErrorMessage = ex.Message;
                return null;
            }
        }

    }
}
