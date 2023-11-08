using ProSalesManager._01_Data.Modules.Marca.Interfaces;
using ProSalesManager._02_Busnisess.Marca.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Marca
{
    public class B_Marca : IB_Marca
    {
        private readonly ISP_Marca _sP_Marca;
        public B_Marca(ISP_Marca sP_Marca)
        {
            _sP_Marca = sP_Marca;
        }
        public List<MarcaModel> ObtenerMarca()
        {
            return _sP_Marca.ObtenerMarca();
        }
        public bool EliminarMarca(int idCalidad)
        {
            return _sP_Marca.EliminarMarca(idCalidad);
        }
        public bool ActualizarMarca(int idCalidad, string descripcion)
        {
            return _sP_Marca.ActualizarMarca(idCalidad, descripcion);
        }
        public bool InsertarMarca(string descripcion)
        {
            return _sP_Marca.InsertarMarca(descripcion);
        }
        public List<ComboBox> ObtenerMarcasParaComboBox()
        {
            return _sP_Marca.ObtenerMarcasParaComboBox();
        }
    }
}
