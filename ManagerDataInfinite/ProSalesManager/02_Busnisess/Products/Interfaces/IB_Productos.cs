using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Products.Interfaces
{
    public interface IB_Productos
    {
        List<ProductoModel> Productos(string usuarioNavegacion);
        bool UpdateProductos(ProductoModel oProductoModel);
        bool InsertProducto(ProductoModel oProductoModel);
    }
}
