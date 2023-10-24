using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using System.Diagnostics.CodeAnalysis;

namespace ProSalesManager._02_Busnisess.Products
{
    public class B_Productos : IB_Productos
    {
        private readonly ISP_Productos _sP_Products;
        public B_Productos(ISP_Productos sP_Products)
        {
            _sP_Products = sP_Products;
        }

        public List<ProductResponse> GetAllProductsDetails(bool Activo)
        {
            var productsList = _sP_Products.ProductosLista(Activo);
            var responseList = new List<ProductResponse>();

            foreach (var product in productsList)
            {
                var productResponse = new ProductResponse
                {
                    Id = product.idProducto,
                    Genero = product.Genero,
                    Producto = product.producto,
                    Marca = product.marca,
                    Precio = product.precio,
                    Cantidad = product.cantidad,
                    Sizes = product.Sizes,
                    Images = product.Images,
                    Colores = product.Colores,
                    Tags = product.Tags,
                    Descripcion = product.Descripcion,
                    NewLabel = new LabelModel
                    {
                        Enabled = product.NewLabelEnabled,
                        Content = product.NewLabelContent
                    },
                    SaleLabel = new LabelModel
                    {
                        Enabled = product.SaleLabelEnabled,
                        Content = product.SaleLabelContent
                    },
                    Activo = product.Activo
                };
                responseList.Add(productResponse);
            }
            return responseList;
        }

        public List<ModeloProductoModel> ModeloProductosListaCrud()
        {
            var productsList = _sP_Products.ModeloProductosListaCrud();
            return productsList;

        }
        public List<ModeloProductoModel> ModeloProductosByIDCrud(int idModeloProducto)
        {
            return _sP_Products.ModeloProductosByIDCrud(idModeloProducto);
        }
        public bool InsertModeloProducto(ModeloProductoModel oModeloProductoModel)
        {
            var insertSizeDetalle = _sP_Products.InsertModeloProducto(oModeloProductoModel);
            return insertSizeDetalle;
        }
        public bool UpdateModeloProducto(ModeloProductoModel oModeloProductoModel)
        {
            var updateSizeDetalle = _sP_Products.UpdateModeloProducto(oModeloProductoModel);
            return updateSizeDetalle;
        }
        public bool DeleteModeloProducto(int idModeloProducto)
        {
            var deleteSizeDetalle = _sP_Products.DeleteModeloProducto(idModeloProducto);
            return deleteSizeDetalle;
        }
        //CRUD
        public List<CrudProductoModel> ProductosListaCrud()
        {
            var productsList = _sP_Products.ProductosListaCrud();
            return productsList;

        }
        public List<CrudProductoModel> ProductosByIDCrud(int idProducto)
        {
            var product = _sP_Products.ProductosByIDCrud(idProducto);
            return product;
        }
        public List<ComboBox> ProveedorCrudCB()
        {
            var proveedorcb = _sP_Products.ProveedorCrudCB();
            return proveedorcb;
        }
        public List<ComboBox> GeneroCrudCB()
        {
            var generocb = _sP_Products.GeneroCrudCB();
            return generocb;
        }
        public List<ComboBox> CategoriaCrudCB()
        {
            var categoriacb = _sP_Products.CategoriaCrudCB();
            return categoriacb;
        }
        public List<ComboBox> ModeloCrudCB()
        {
            var modelocb = _sP_Products.ModeloCrudCB();
            return modelocb;
        }
        public int UpdateProducto(CrudProductoModel oCrudProductoModel)
        {
            int producto = _sP_Products.UpdateProducto(oCrudProductoModel);
            return producto;
        }
        public bool DeleteProducto(int idProducto)
        {
            var producto = _sP_Products.DeleteProducto(idProducto);
            return producto;
        }


        public List<CrudSizeDetalleModel> SizeDetalleByIDCrud(int idProducto)
        {
            var sizeDetalle = _sP_Products.SizeDetalleByIDCrud(idProducto);
            return sizeDetalle;
        }
        public List<ComboBox> SizeCrudCB()
        {
            var sizeCrudCB = _sP_Products.SizeCrudCB();
            return sizeCrudCB;
        }
        public bool InsertSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel)
        {
            var insertSizeDetalle = _sP_Products.InsertSizeDetalle(oSizeDetalleModel);
            return insertSizeDetalle;
        }
        public bool UpdateSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel)
        {
            var updateSizeDetalle = _sP_Products.UpdateSizeDetalle(oSizeDetalleModel);
            return updateSizeDetalle;
        }
        public bool DeleteSizeDetalle(int idSizeDetalle)
        {
            var deleteSizeDetalle = _sP_Products.DeleteSizeDetalle(idSizeDetalle);
            return deleteSizeDetalle;
        }

        public List<CrudTagDetalleModel> TagsByIDCrud(int idProducto)
        {
            var tags = _sP_Products.TagsByIDCrud(idProducto);
            return tags;
        }
        public bool InsertTagCrud(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            var insert =_sP_Products.InsertTagCrud(oCrudTagDetalleModel);
            return insert;
            
        }
        public bool UpdateTagCrud(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            return _sP_Products.UpdateTagCrud(oCrudTagDetalleModel);
        }
        public bool DeleteTagCrud(int id)
        {
           return _sP_Products.DeleteTagCrud(id);
        }

        // COLOR DETALLE
        public List<ComboBox> ColorDetalleCrudCB()
        {
            return _sP_Products.ColorDetalleCrudCB();
        }
        public List<CrudColorDetalleModel> ColorDetalleByIDCrud(int idProducto)
        {
            return _sP_Products.ColorDetalleByIDCrud(idProducto);
        }
        public bool InsertColorDetalleCrud(CrudColorDetalleModel crudColorDetalleModel)
        {
            bool result = _sP_Products.InsertColorDetalleCrud(crudColorDetalleModel);
            return result;
        }
        public bool UpdateColorDetalleCrud(CrudColorDetalleModel crudColorDetalleModel)
        {
            return _sP_Products.UpdateColorDetalleCrud(crudColorDetalleModel);
        }
        public bool DeleteColorDetalleCrud(int id)
        {
            return _sP_Products.DeleteColorDetalleCrud(id);
        }



        public List<CrudImagenModel> ImagenByIDCrud(int idProducto)
        {
            var imagenes = _sP_Products.ImagenByIDCrud(idProducto);
            return imagenes;
        }

        public bool InsertImagen(ImagenModel oImagenModel)
        {
            bool result = _sP_Products.InsertImagenes(oImagenModel);
            return result;
        }

        public bool DeleteImagenes(int idImagenes)
        {
            return _sP_Products.DeleteImagenes(idImagenes);
        }

        public bool InsertProducto(ProductoModel oProductoModel)
        {
            bool result = _sP_Products.InsertProducto(oProductoModel);
            return result;
        }

        public List<ColorModel> GetAllColors()
        {
            return _sP_Products.GetAllColors();
        }

        public bool InsertColor(ColorModel colorModel)
        {
            return _sP_Products.InsertColor(colorModel);
        }

        public bool UpdateColor(ColorModel colorModel)
        {
            return _sP_Products.UpdateColor(colorModel);
        }

        public bool DeleteColor(int idColor)
        {
            return _sP_Products.DeleteColor(idColor);  // Asumiendo que idColor es una propiedad de ColorModel
        }

        // Implementación de métodos para Size
        public List<SizeModel> GetAllSizes()
        {
            return _sP_Products.GetAllSizes();
        }
        public bool DeleteSize(int idSize)
        {
            return _sP_Products.DeleteSize(idSize);  
        }
        public bool UpdateSize(SizeModel sizeModel)
        {
            return _sP_Products.UpdateSize(sizeModel);
        }
        public bool InsertSize(SizeModel sizeModel)
        {
            return _sP_Products.InsertSize(sizeModel);
        }



        // Implementación de métodos para Proveedor

        public List<ProveedoresModel> GetAllProveedores()
        {
            return _sP_Products.GetAllProveedores();
        }

        public bool InsertProveedor(ProveedoresModel proveedorModel)
        {
            return _sP_Products.InsertProveedor(proveedorModel);
        }

        public bool UpdateProveedor(ProveedoresModel proveedorModel)
        {
            return _sP_Products.UpdateProveedor(proveedorModel);
        }

        public bool DeleteProveedor(int idProveedor)
        {
            return _sP_Products.DeleteProveedor(idProveedor);
        }
    }
}
