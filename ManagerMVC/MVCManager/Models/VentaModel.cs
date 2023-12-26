namespace MVCManager.Models
{
    public class VentaModel
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public int IdTipoPago { get; set; }
        public string TipoPago { get; set; }
        public DateTime HoraCreacion { get; set; }
        public DateTime HoraActualizacion { get; set; }
        public string TotalDefinido { get; set; }
        public int IdEmpresaTranspte { get; set; }
        public string NombreEmpresaTransporte { get; set; }
        public int IdUbigeo { get; set; }
        public string NombreDepartamento { get; set; }
    }
    public class VentaResponse
    {
        public List<VentaModel> Result { get; set; }
    }
}
