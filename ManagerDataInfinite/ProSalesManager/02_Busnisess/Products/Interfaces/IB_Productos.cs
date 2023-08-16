using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Products.Interfaces
{
    public interface IB_Productos
    {
        List<ProductResponse> GetAllProductsDetails(bool Activo);
        bool UpdateProductos(ProductoModel oProductoModel);
        bool InsertProducto(ProductoModel oProductoModel);
    }
}
