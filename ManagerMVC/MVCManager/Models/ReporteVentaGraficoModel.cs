namespace MVCManager.Models
{
    public class ReporteVentaGraficoModel
    {
        public int idVenta { get; set; }
        public DateTime fechaVenta { get; set; }
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public int idUsuario { get; set; }
        public int idEmpresa { get; set; }
        public int idTipoPago { get; set; }
        public string tipoPago { get; set; }
        public DateTime horaCreacion { get; set; }
        public DateTime horaActualizacion { get; set; }
        public string totalDefinido { get; set; }
        public int idEmpresaTranspte { get; set; }
        public string nombreEmpresaTransporte { get; set; }
        public int idUbigeo { get; set; }
        public string nombreDepartamento { get; set; }
    }
}
