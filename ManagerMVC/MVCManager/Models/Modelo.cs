namespace MVCManager.Models
{
    public class Modelo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class ModeloResponse
    {
        public List<Modelo> Result { get; set; }
    }

}
