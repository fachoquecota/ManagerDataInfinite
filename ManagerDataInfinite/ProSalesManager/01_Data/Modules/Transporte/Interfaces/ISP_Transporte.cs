using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Transporte.Interfaces
{
    public interface ISP_Transporte
    {
        List<ComboBox> ObtenerEmpresaTransporteComboBox();
        bool InsertarEmpresaTransporte(string descripcion, string direccion);
        bool ActualizarEmpresaTransporte(int idEmpresaTranspte, string descripcion, string direccion);
        bool EliminarEmpresaTransporte(int idEmpresaTranspte);
        List<EmpresaTransporteModel> ObtenerEmpresaTransporte();
    }
}
