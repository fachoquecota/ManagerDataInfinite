using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Customers.Interfaces
{
    public interface IB_Clientes
    {
        List<ClienteModel> Clientes();
        List<ClienteModel> ClientesById(int idCliente);
        bool UpdateCliente(ClienteModel oProductoModel);
        int? InsertCliente(ClienteModel oProductoModel);
        bool DeleteCliente(int idCliente);
        List<ComboBox> TipoDocumento();
    }
}
