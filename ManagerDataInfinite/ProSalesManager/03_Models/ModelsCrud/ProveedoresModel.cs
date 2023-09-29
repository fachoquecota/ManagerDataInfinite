namespace ProSalesManager._03_Models.ModelsCrud
{
    public class ProveedoresModel
    {
        public int idProveedor { get; set; }
        public string? proveedor { get; set; }
        public string? contacto { get; set; }
        public string? telefono { get; set; }
        public string? direccion { get; set; }
        public DateTime fecha { get; set; }
        public DateTime horaCreacion { get; set; }
        public DateTime horaActualizacion { get; set; }
        public int idEmpresa { get; set; }
    }
}
