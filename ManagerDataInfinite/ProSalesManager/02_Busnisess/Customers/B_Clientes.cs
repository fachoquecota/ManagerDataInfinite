using ProSalesManager._01_Data.Modules.Customers.Interfaces;
using ProSalesManager._02_Busnisess.Customers.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Customers
{
    public class B_Clientes : IB_Clientes
    {
        private readonly ISP_Clientes _sP_Clientes;
        public B_Clientes(ISP_Clientes sP_Clientes)
        {
            _sP_Clientes = sP_Clientes;
        }
        public List<ClienteModel> Clientes()
        {
            var resultSP = _sP_Clientes.ClientesLista();
            return resultSP;
        }
        public List<ClienteModel> ClientesById(int idCliente)
        {
            var resultSP = _sP_Clientes.ClientesListaByid(idCliente);
            return resultSP;
        }
        public bool UpdateCliente(ClienteModel oProductoModel)
        {
            bool result = _sP_Clientes.UpdateCliente(oProductoModel);
            return result;
        }
        public bool InsertCliente(ClienteModel oProductoModel)
        {
            bool result = _sP_Clientes.InsertCliente(oProductoModel);
            return result;
        }


        public List<ComboBox> TipoDocumento()
        {
            var resultSP = _sP_Clientes.TipoDocumentoComboBox();
            return resultSP;
        }
    }
}
