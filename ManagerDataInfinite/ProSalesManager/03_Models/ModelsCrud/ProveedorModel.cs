namespace ProSalesManager._03_Models.ModelsCrud
{
    public class ProveedorModel
    {
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraCreacion { get; set; }
        public DateTime HoraActualizacion { get; set; }
        public int IdEmpresa { get; set; }
    }
}
