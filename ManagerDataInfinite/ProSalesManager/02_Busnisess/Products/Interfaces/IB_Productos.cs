using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Products.Interfaces
{
    public interface IB_Productos
    {
        List<ProductResponse> GetAllProductsDetails(bool Activo);
        //Crud
        List<CrudProductoModel> ProductosListaCrud();
        List<CrudProductoModel> ProductosByIDCrud(int idProducto);
        List<ComboBox> ProveedorCrudCB();
        List<ComboBox> GeneroCrudCB();
        List<ComboBox> CategoriaCrudCB();
        bool UpdateProducto(CrudProductoModel oCrudProductoModel);
        bool DeleteProducto(int idProducto);

        bool InsertProducto(ProductoModel oProductoModel);

    }
}
