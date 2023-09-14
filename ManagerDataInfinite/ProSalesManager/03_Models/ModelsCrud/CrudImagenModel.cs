namespace ProSalesManager._03_Models.ModelsCrud
{
    public class CrudImagenModel
    {
       public int  idImagenes { get; set; } 
       public int  idProducto { get; set; }
       public string  rutaImagen { get; set; }
       public string  nombreImagen { get; set; }
       public bool  Activo { get; set; }
    }
}
