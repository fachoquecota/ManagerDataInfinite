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
        public bool Activo { get; set; }
    }
}
