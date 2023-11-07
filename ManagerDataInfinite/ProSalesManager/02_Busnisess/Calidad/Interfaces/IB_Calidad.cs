using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Calidad.Interfaces
{
    public interface IB_Calidad
    {
        public List<CalidadModel> ObtenerCalidad();
        bool DeleteCalidad(int idCalidad);
        public bool UpdateCalidad(int idCalidad, string descripcion);
        public bool InsertCalidad(string descripcion);
    }
}
