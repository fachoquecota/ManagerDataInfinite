namespace ProSalesManager._03_Models
{
    public class Venta
    {
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTipoPago { get; set; }
        public int idTransporteCombobox { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public decimal? TotalDefinido { get; set; } 
        public List<DetalleVenta> DetallesVenta { get; set; }
    }
    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class Ubicacion
    {
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
    }
}
