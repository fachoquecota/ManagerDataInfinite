namespace MVCManager.Models
{
    public class ProductoUpdateModel
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public string Marca { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public int IdProveedor { get; set; }
        public int IdEmpresa { get; set; }
        public string ImagenCarpeta { get; set; }
        public string ImagenNombre { get; set; }
        public bool Activo { get; set; }
        public int IdGenero { get; set; }
        public int IdCategoria { get; set; }
    }
}
