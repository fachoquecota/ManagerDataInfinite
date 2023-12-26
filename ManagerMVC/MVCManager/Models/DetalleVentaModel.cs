namespace MVCManager.Models
{
    public class DetalleVentaModel
    {
        public int IdVentaDetalle { get; set; }
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int IdModeloProducto { get; set; }
        public string NombreModeloProducto { get; set; }
        public int IdCalidad { get; set; }
        public string NombreCalidad { get; set; }
        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }
        public int Cantidad { get; set; }
    }
    public class DetalleVentaResponse
    {
        public List<DetalleVentaModel> Result { get; set; }
    }
}
