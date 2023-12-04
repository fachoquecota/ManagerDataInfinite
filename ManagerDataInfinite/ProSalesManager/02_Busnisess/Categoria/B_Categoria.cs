using ProSalesManager._01_Data.Modules.Categoria.Interfaces;
using ProSalesManager._02_Busnisess.Categoria.Interfaces;
using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Categoria
{
    public class B_Categoria : IB_Categoria
    {
        private readonly ISP_Categoria _sP_Categoria;
        public B_Categoria(ISP_Categoria sP_Categoria)
        {
            _sP_Categoria = sP_Categoria;
        }
        public List<CategoriaModel> ObtenerCategoria()
        {
            return _sP_Categoria.ObtenerCategoria();
        }
        public bool EliminarCategoria(int idCategoria)
        {
            return _sP_Categoria.EliminarCategoria(idCategoria);
        }
        public bool ActualizarCategoria(int idCategoria, string descripcion)
        {
            return _sP_Categoria.ActualizarCategoria(idCategoria, descripcion);
        }
        public bool InsertarCategoria(string descripcion)
        {
            return _sP_Categoria.InsertarCategoria(descripcion);
        }
    }
}
