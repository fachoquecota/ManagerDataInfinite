namespace ProSalesManager._03_Models.PublicProducts
{
    public class PublicSizeModel
    {
        public int IdSize { get; set; }
        public List<string> Descripciones { get; set; }
        public List<PublicColorModel> Colores { get; set; } = new List<PublicColorModel>();
    }
}
