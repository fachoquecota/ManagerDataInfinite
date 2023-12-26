namespace MVCManager.Models
{
    public class DashboardViewModel
    {
        public List<VentaModel> Ventas { get; set; }
        public List<DetalleVentaModel> DetalleVentas { get; set; }
        public string DepartamentoConMasVentas { get; set; }
        public string ModeloProductoMasVendido { get; set; }
        public string MarcaMasVendida { get; set; }
        public Dictionary<string, decimal> VentasPorMes { get; set; }

        public Dictionary<string, int> VentasPorCalidad { get; set; }
        public Dictionary<string, int> VentasPorModelo { get; set; }

    }
}
