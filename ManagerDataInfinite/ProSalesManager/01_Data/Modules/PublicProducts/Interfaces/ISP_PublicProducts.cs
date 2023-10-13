using ProSalesManager._03_Models.PublicProducts;

namespace ProSalesManager._01_Data.Modules.PublicProducts.Interfaces
{
    public interface ISP_PublicProducts
    {
        PublicProducsModel ObtenerProductoPorId(bool Activo, int idProducto);
    }
}
