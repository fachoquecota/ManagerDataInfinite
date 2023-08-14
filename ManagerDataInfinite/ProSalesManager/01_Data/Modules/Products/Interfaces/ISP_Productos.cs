using ProSalesManager._03_Models;

namespace ProSalesManager._01_Data.Modules.Products.Interfaces
{
    public interface ISP_Productos
    {
        List<ProductoModel> ProductosLista(string usuarioNavegacion);
        bool UpdateProducto(ProductoModel oProductoModel);
        bool InsertProducto(ProductoModel oProductoModel);
    }
}
