namespace ProSalesManager._03_Models
{
    public class FiltroVentasModel
    {
        public int? IdCliente { get; set; }
        public int? idEmpresaTranspte { get; set; }

        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
