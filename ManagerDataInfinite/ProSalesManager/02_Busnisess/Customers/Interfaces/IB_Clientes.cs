using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Customers.Interfaces
{
    public interface IB_Clientes
    {
        List<ClienteModel> Clientes(string usuarioNavegacion);
        bool UpdateCliente(ClienteModel oProductoModel);
        bool InsertCliente(ClienteModel oProductoModel);
    }
}
