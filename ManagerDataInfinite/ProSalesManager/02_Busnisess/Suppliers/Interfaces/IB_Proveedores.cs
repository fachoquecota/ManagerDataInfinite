using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Suppliers.Interfaces
{
    public interface IB_Proveedores
    {
        List<ProveedoresModel> Proveedores(string usuarioNavegacion);
        bool UpdateProveedor(ProveedoresModel oProductoModel);
        bool InsertProveedor(ProveedoresModel oProductoModel);
    }
}
