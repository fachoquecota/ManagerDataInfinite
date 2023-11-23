using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Customers.Interfaces
{
    public interface ISP_Clientes
    {
        List<ClienteModel> ClientesLista();
        List<ClienteModel> ClientesListaByid(int idCliente);
        bool UpdateCliente(ClienteModel oClienteModel);
        bool InsertCliente(ClienteModel oClienteModel);

        List<ComboBox> TipoDocumentoComboBox();
    }
}
