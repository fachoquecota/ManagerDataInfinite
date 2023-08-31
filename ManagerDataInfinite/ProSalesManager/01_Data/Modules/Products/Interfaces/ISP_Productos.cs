using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Products.Interfaces
{
    public interface ISP_Productos
    {
        List<ProductoModel> ProductosLista(bool Activo);
        List<ImagenModel> ImagenesLista(int idProducto);
        List<TagModel> TagsLista(int idProducto);
        List<SizeModel> SizesLista(int idProducto);
        List<DescripcionModel> DescripcionesLista(int idProducto);
        bool UpdateProducto(ProductoModel oProductoModel);
        bool UpdateImagenes(ImagenModel oImagenModel);
        bool UpdateTag(TagModel oTagModel);
        bool UpdateSize(SizeModel oSizeModel);
        bool UpdateDescripcion(DescripcionModel oDescripcionModel);
        bool InsertProducto(ProductoModel oProductoModel);
        bool InsertImagenes(ImagenModel oImagenModel);
        bool InsertTag(TagModel oTagModel);
        bool InsertSize(SizeModel oSizeModel);
        bool InsertDescripcion(DescripcionModel oDescripcionModel);


        //CRUD
        List<CrudProductoModel> ProductosListaCrud();
    }
}
