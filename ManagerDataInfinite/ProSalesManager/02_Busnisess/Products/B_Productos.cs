using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

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

        //public List<ProductoModel> GetAllProductsDetails(string usuarioNavegacion)
        //{
        //    var resultSP = _sP_Products.ProductosLista(usuarioNavegacion);
        //    return resultSP;
        //}
        public bool UpdateProductos(ProductoModel oProductoModel)
        {
            bool result = _sP_Products.UpdateProducto(oProductoModel);
            return result;
        }
        public bool InsertProducto(ProductoModel oProductoModel)
        {
            bool result = _sP_Products.InsertProducto(oProductoModel);
            return result;
        }
    }
}
