namespace ProSalesManager._03_Models.ModelsCrud
{
    public class CrudSizeDetalleModel
    {
        public int idSizeDetalle { get; set; }
        public int idSize { get; set; }
        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
