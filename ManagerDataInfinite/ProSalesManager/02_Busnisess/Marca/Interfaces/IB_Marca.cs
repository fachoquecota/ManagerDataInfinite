using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Marca.Interfaces
{
    public interface IB_Marca
    {
        public List<MarcaModel> ObtenerMarca();
        public bool EliminarMarca(int idCalidad);
        public bool ActualizarMarca(int idCalidad, string descripcion);
        public bool InsertarMarca(string descripcion);
        List<ComboBox> ObtenerMarcasParaComboBox();
    }
}
