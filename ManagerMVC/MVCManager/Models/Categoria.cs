namespace MVCManager.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    public class CategoriaResponse
    {
        public List<Categoria> result { get; set; }
    }
}
