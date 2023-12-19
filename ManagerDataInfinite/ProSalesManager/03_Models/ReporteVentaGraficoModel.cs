namespace ProSalesManager._03_Models
{
    public class ReporteVentaGraficoModel
    {
		public int idVenta { get; set; }
		public DateTime fechaVenta { get; set; }
		public int idCliente { get; set; }
		public string NombreCliente { get; set; }
		public int idUsuario { get; set; } 
		public int idEmpresa { get; set; } 
		public int idTipoPago { get; set; }
		public string TipoPago { get; set; }
		public DateTime horaCreacion { get; set; }
		public DateTime horaActualizacion { get; set; }
		public string TotalDefinido { get; set; }
		public int idEmpresaTranspte { get; set; }
		public string NombreEmpresaTransporte { get; set; }
		public int idUbigeo { get; set; }
		public string NombreDepartamento { get; set; }

	}
}
