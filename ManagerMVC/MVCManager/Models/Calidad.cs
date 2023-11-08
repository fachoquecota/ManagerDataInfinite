namespace MVCManager.Models
{
    public class Calidad
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
    public class CalidadResponse
    {
        public List<Calidad> calidad { get; set; }
    }
}
