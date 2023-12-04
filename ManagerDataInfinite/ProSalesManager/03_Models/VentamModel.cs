namespace ProSalesManager._03_Models
{
    public class Venta
    {
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTipoPago { get; set; }
        public decimal? TotalDefinido { get; set; } // Nullable para permitir null
        public List<DetalleVenta> DetallesVenta { get; set; }
    }
    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
