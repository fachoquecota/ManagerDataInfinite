namespace ProSalesManager._03_Models
{
    public class ProductoVenta
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public string Marca { get; set; }
        public decimal Precio { get; set; }
        public string ImagenCarpeta { get; set; }
        public string ImagenNombre { get; set; }
        public int IdSize { get; set; }
        public string SizeDescription { get; set; }
        public int IdColor { get; set; }
        public string ColorDescription { get; set; }
        public int IdModeloProducto { get; set; }
        public string ModeloDescripcion { get; set; }
    }
}
