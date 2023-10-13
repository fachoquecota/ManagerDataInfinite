using ProSalesManager._03_Models.PublicProducts;

namespace ProSalesManager._02_Busnisess.PublicProducts.Interfaces
{
    public interface IB_PublicProducts
    {
        PublicProducsModel ProductsData(bool Activo, int idProducto);
    }
}
