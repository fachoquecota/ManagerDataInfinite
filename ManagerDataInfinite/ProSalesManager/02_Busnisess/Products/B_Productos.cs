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
        public List<ProductoModel> Productos(string usuarioNavegacion)
        {
            var resultSP = _sP_Products.ProductosLista(usuarioNavegacion);
            return resultSP;
        }
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
