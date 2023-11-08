using ProSalesManager._01_Data.Modules.Calidad.Interfaces;
using ProSalesManager._02_Busnisess.Calidad.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Calidad
{
    public class B_Calidad : IB_Calidad
    {
        private readonly ISP_Calidad _sP_Calidad;
        public B_Calidad(ISP_Calidad sP_Calidad)
        {
            _sP_Calidad = sP_Calidad;
        }
        public List<CalidadModel> ObtenerCalidad()
        {
            return _sP_Calidad.ObtenerCalidad();
        }
        public bool DeleteCalidad(int idCalidad)
        {
            return _sP_Calidad.EliminarCalidad(idCalidad);
        }
        public bool UpdateCalidad(int idCalidad, string descripcion)
        {
            return _sP_Calidad.ActualizarCalidad(idCalidad, descripcion);
        }
        public bool InsertCalidad(string descripcion)
        {
            return _sP_Calidad.InsertarCalidad(descripcion);
        }
        public List<ComboBox> ObtenerCalidadesParaComboBox()
        {
            return _sP_Calidad.ObtenerCalidadesParaComboBox();

        }
    }
}
