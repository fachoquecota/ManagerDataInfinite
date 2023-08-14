using ProSalesManager._01_Data.Modules.Supplier.Interfaces;
using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Suppliers
{
    public class B_Proveedores
    {
        private readonly ISP_Proveedores _sP_Proveedores;
        public B_Proveedores(ISP_Proveedores sP_Proveedores)
        {
            _sP_Proveedores = sP_Proveedores;
        }
        public List<ProveedoresModel> Proveedores(string usuarioNavegacion)
        {
            var resultSP = _sP_Proveedores.ProveedoresLista(usuarioNavegacion);
            return resultSP;
        }
        public bool UpdateProveedor(ProveedoresModel oProductoModel)
        {
            bool result = _sP_Proveedores.UpdateProveedor(oProductoModel);
            return result;
        }
        public bool InsertProveedor(ProveedoresModel oProductoModel)
        {
            bool result = _sP_Proveedores.InsertProveedor(oProductoModel);
            return result;
        }
    }
}
