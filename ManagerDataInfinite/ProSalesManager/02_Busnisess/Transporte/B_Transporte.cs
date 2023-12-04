using ProSalesManager._01_Data.Modules.Transporte.Interfaces;
using ProSalesManager._02_Busnisess.Transporte.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Transporte
{
    public class B_Transporte : IB_Transporte
    {
        private readonly ISP_Transporte _sP_Transporte;
        public B_Transporte(ISP_Transporte sP_Transporte)
        {
            _sP_Transporte = sP_Transporte;
        }
        public List<ComboBox> ObtenerEmpresaTransporteComboBox()
        {
            var result = _sP_Transporte.ObtenerEmpresaTransporteComboBox();
            return result;
        }
        public bool InsertarEmpresaTransporte(string descripcion, string direccion)
        {
            var result = _sP_Transporte.InsertarEmpresaTransporte(descripcion, direccion);
            return result;
        }
        public bool ActualizarEmpresaTransporte(int idEmpresaTranspte, string descripcion, string direccion)
        {
            var result = _sP_Transporte.ActualizarEmpresaTransporte(idEmpresaTranspte, descripcion, direccion);
            return result;
        }
        public bool EliminarEmpresaTransporte(int idEmpresaTranspte)
        {
            var result = _sP_Transporte.EliminarEmpresaTransporte(idEmpresaTranspte);
            return result;
        } 
        public List<EmpresaTransporteModel> ObtenerEmpresaTransporte()
        {
            var result = _sP_Transporte.ObtenerEmpresaTransporte();
            return result;
        }
    }
}
