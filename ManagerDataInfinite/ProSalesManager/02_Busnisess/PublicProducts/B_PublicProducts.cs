
using ProSalesManager._01_Data.Modules.PublicProducts.Interfaces;
using ProSalesManager._02_Busnisess.PublicProducts.Interfaces;
using ProSalesManager._03_Models.PublicProducts;

namespace ProSalesManager._02_Busnisess.PublicProducts
{
    public class B_PublicProducts : IB_PublicProducts
    {
        private readonly ISP_PublicProducts _sP_Products;
        public B_PublicProducts(ISP_PublicProducts sP_Products)
        {
            _sP_Products = sP_Products;
        }
        public PublicProducsModel ProductsData(bool Activo, int idProducto)
        {
            var product = _sP_Products.ObtenerProductoPorId(Activo, idProducto);

            // Si necesitas convertir de ProductoModel a PublicProducsModel, puedes hacerlo aquí.
            // Por ahora, asumiré que son el mismo tipo de modelo o que tienen una estructura similar.

            return product;
        }

    }
}
