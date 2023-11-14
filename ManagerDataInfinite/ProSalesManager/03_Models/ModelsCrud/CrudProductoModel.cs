namespace ProSalesManager._03_Models.ModelsCrud
{
    public class CrudProductoModel
    {
        public int idProducto { get; set; }
        public string producto { get; set; }
        public string marca { get; set; }
        public decimal costo { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public int idProveedor { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int idEmpresa { get; set; }
        public string? imagenCarpeta { get; set; }
        public string? imagenNombre { get; set; }
        public DateTime horaCreacion { get; set; }
        public DateTime horaActualizacion { get; set; }
        public bool Activo { get; set; }
        public int idGenero { get; set; }
        public int idCategoria { get; set; }
        public int idModeloProducto { get; set; }
        public int idCalidad { get; set; }

    }
}
