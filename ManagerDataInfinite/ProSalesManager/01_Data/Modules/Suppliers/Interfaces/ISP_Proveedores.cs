using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Supplier.Interfaces
{
    public interface ISP_Proveedores
    {
        List<ProveedoresModel> ProveedoresLista(string usuarioNavegacion);
        bool UpdateProveedor(ProveedoresModel oProveedoresModel);
        bool InsertProveedor(ProveedoresModel oProveedoresModel);
    }
}
