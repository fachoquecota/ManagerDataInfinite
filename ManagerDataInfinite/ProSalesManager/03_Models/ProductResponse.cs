namespace ProSalesManager._03_Models
{
    public class ProductResponse
    {
        public int id { get; set; }
        public string gender { get; set; }
        public string publish {  get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string Marca { get; set; }
        public double price { get; set; }
        public int available { get; set; }
        public string priceSale { get; set; }
        public int taxes { get; set; }
        public int quantity { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime horaActualizacion { get; set; }
        public string imagenCarpeta { get; set; }
        public string imagenNombre { get; set; }
        public List<string> sizes { get; set; }
        public string inventoryType { get; set; }
        public List<string> images { get; set; }
        public List<string> tags { get; set; }
        public string code { get; set; }
        public List<string> colors { get; set; }
        public string descripcion { get; set; }
        public LabelModel newLabel { get; set; }
        public string sku { get; set; }
        public LabelModel saleLabel { get; set; }
        public bool Activo { get; set; }
        public string coverUrl { get; set; }
        public double totalRatings { get; set; }
        public int totalSold { get; set; }
        public int totalReviews { get; set; }
        public string subDescription { get; set; }
    }
    public class LabelModel
    {
        public bool enabled { get; set; }
        public string content { get; set; }
    }
}
