using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Calidad.Interfaces
{
    public interface ISP_Calidad
    {
        public List<CalidadModel> ObtenerCalidad();
        public List<ComboBox> ObtenerCalidadesParaComboBox();
        public bool InsertarCalidad(string descripcion);
        public bool ActualizarCalidad(int idCalidad, string descripcion);
        public bool EliminarCalidad(int idCalidad);
    }
}
