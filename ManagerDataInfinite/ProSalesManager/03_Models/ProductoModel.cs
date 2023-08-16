namespace ProSalesManager._03_Models
{
    public class ProductoModel
    {
        public int idProducto { get; set; }
        public string producto { get; set; }
        public string marca { get; set; }
        public double precio { get; set; }
        public int cantidad { get; set; }
        public int idProveedor { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int idEmpresa { get; set; }
        public string imagenCarpeta { get; set; }
        public string imagenNombre { get; set; }
        public DateTime horaCreacion { get; set; }
        public DateTime horaActualizacion { get; set; }
        public bool Activo { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
        public string Descripcion { get; set; }
        public string Genero { get; set; }
        public string NewLabelContent { get; set; }
        public bool NewLabelEnabled { get; set; }
        public string SaleLabelContent { get; set; }
        public bool SaleLabelEnabled { get; set; }
    }

}
