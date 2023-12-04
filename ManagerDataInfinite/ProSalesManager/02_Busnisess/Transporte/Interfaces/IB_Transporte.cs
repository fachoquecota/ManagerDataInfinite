using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Transporte.Interfaces
{
    public interface IB_Transporte
    {
        List<ComboBox> ObtenerEmpresaTransporteComboBox();
        bool InsertarEmpresaTransporte(string descripcion, string direccion);
        bool ActualizarEmpresaTransporte(int idEmpresaTranspte, string descripcion, string direccion);
        bool EliminarEmpresaTransporte(int idEmpresaTranspte);
        List<EmpresaTransporteModel> ObtenerEmpresaTransporte();
    }
}
