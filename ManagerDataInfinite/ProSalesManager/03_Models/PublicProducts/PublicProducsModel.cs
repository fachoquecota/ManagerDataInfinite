namespace ProSalesManager._03_Models.PublicProducts
{
    public class PublicProducsModel
    {
        public int idProducto { get; set; }
        public bool Activo { get; set; }
        public string producto { get; set; }
        public string marca { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string imagenCarpeta { get; set; }
        public string imagenNombre { get; set; }
        public DateTime horaCreacion { get; set; }
        public DateTime horaActualizacion { get; set; }
        public int idProveedor { get; set; }
        public string proveedor { get; set; }
        public string Categoria { get; set; }
        public List<string> Images { get; set; }
        public List<PublicSizeModel> Sizes { get; set; } = new List<PublicSizeModel>();
        public string descripcion { get; set; }
        public string Genero { get; set; }
        public string NewLabelContent { get; set; }
        public bool NewLabelEnabled { get; set; }
        public string SaleLabelContent { get; set; }
        public bool SaleLabelEnabled { get; set; }
    }
}
