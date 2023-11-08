namespace MVCManager.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    public class MarcaResponse
    {
        public List<Marca> calidad { get; set; }
    }
}
