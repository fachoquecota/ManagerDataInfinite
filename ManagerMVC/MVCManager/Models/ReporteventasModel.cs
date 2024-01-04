namespace MVCManager.Models
{
    public class ReporteventasModel
    {
        public int idVenta { get; set; }
        public int idCliente { get; set; }
        public string nombres { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string telefono { get; set; }
        public int idTipoPago { get; set; }
        public string tipoPago { get; set; }
        public int cantidad { get; set; }
        public string colorDescription { get; set; }
        public string modeloDescripcion { get; set; }
        public string calidadDescripcion { get; set; }
        public string marcaDescripcion { get; set; }
        public string categoriaDescripcion { get; set; }
        public string empresaTransporte { get; set; }
        public int idEmpresa { get; set; }
        public decimal total { get; set; }
        public string fechaVenta { get; set; }
        public int idUbigeo { get; set; }
        public string ubigeo { get; set; }
        public string direccion { get; set; }

    }

    public class ApiResponseVentas
    {
        public List<ReporteventasModel> result { get; set; }
    }

}
