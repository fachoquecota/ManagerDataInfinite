using ProSalesManager._03_Models;

namespace ProSalesManager._01_Data.Modules.Customers.Interfaces
{
    public interface ISP_Clientes
    {
        List<ClienteModel> ClientesLista(string usuarioNavegacion);
        bool UpdateCliente(ClienteModel oClienteModel);
        bool InsertCliente(ClienteModel oClienteModel);
    }
}
