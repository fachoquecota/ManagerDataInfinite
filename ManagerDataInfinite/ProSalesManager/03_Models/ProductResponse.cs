namespace ProSalesManager._03_Models
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public string Descripcion { get; set; }
        public LabelModel NewLabel { get; set; }
        public LabelModel SaleLabel { get; set; }
    }
    public class LabelModel
    {
        public bool Enabled { get; set; }
        public string Content { get; set; }
    }
}
