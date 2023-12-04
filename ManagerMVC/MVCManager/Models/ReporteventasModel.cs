namespace MVCManager.Models
{
    public class ReporteventasModel
    {
        public int idVenta { get; set; }

        public int idCliente { get; set; }

        public string nombres { get; set; }

        public int idTipoPago { get; set; }

        public string tipoPago { get; set; }

        public int idEmpresa { get; set; }

        public decimal total { get; set; }

        public string fechaVenta { get; set; }
    }
    public class ApiResponseVentas
    {
        public List<ReporteventasModel> result { get; set; }
    }
}
