using ProSalesManager._03_Models;

namespace ProSalesManager._01_Data.Modules.Categoria.Interfaces
{
    public interface ISP_Categoria
    {
        List<CategoriaModel> ObtenerCategoria();
        bool InsertarCategoria(string descripcion);
        bool ActualizarCategoria(int idCategoria, string descripcion);
        bool EliminarCategoria(int idCategoria);
    }
}
