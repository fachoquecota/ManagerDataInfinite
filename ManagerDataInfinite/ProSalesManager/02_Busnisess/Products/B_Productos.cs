using ProSalesManager._01_Data.Modules.Products.Interfaces;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models;

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
                    Producto = product.producto,
                    Marca = product.marca,
                    Precio = product.precio,
                    Cantidad = product.cantidad,
                    Sizes = product.Sizes,
                    Images = product.Images,
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
                    }
                };
                responseList.Add(productResponse);
            }
            return responseList;
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
