using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Marca.Interfaces
{
    public interface ISP_Marca
    {
        public List<MarcaModel> ObtenerMarca();
        public List<ComboBox> ObtenerMarcasParaComboBox();
        public bool InsertarMarca(string descripcion);
        public bool ActualizarMarca(int idMarca, string descripcion);
        public bool EliminarMarca(int idMarca);
    }
}
