using ProSalesManager._03_Models;

namespace ProSalesManager._02_Busnisess.Categoria.Interfaces
{
    public interface IB_Categoria
    {
        List<CategoriaModel> ObtenerCategoria();
        bool InsertarCategoria(string descripcion);
        bool ActualizarCategoria(int idCategoria, string descripcion);
        bool EliminarCategoria(int idCategoria);
    }
}
