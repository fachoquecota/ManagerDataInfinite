using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Products.Interfaces
{
    public interface ISP_Productos
    {
        List<ProductoModel> ProductosLista(bool Activo);
        List<ImagenModel> ImagenesLista(int idProducto);
        List<TagModel> TagsLista(int idProducto);
        //List<SizeModel> SizesLista(int idProducto);
        List<DescripcionModel> DescripcionesLista(int idProducto);
        bool UpdateImagenes(ImagenModel oImagenModel);
        bool UpdateTag(TagModel oTagModel);
        
        bool UpdateDescripcion(DescripcionModel oDescripcionModel);
        bool InsertProducto(ProductoModel oProductoModel);
        bool InsertTag(TagModel oTagModel);
        
        bool InsertDescripcion(DescripcionModel oDescripcionModel);


        //CRUD
        List<CrudProductoModel> ProductosListaCrud();
        List<CrudProductoModel> ProductosByIDCrud(int idProducto);
        List<ComboBox> ProveedorCrudCB();
        List<ComboBox> GeneroCrudCB();
        List<ComboBox> CategoriaCrudCB();
        bool UpdateProducto(CrudProductoModel oCrudProductoModel);
        bool DeleteProducto(int idProducto);

        //Size Detalle
        List<CrudSizeDetalleModel> SizeDetalleByIDCrud(int idProducto);
        List<ComboBox> SizeCrudCB();
        bool InsertSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel);
        bool UpdateSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel);
        bool DeleteSizeDetalle(int idSizeDetalle);

        //Tags
        List<CrudTagDetalleModel> TagsByIDCrud(int idProducto);
        bool InsertTagCrud(CrudTagDetalleModel oCrudTagDetalleModel);
        bool UpdateTagCrud(CrudTagDetalleModel oCrudTagDetalleModel);
        bool DeleteTagCrud(int id);

        //Color Detalle
        List<ComboBox> ColorDetalleCrudCB();
        List<CrudColorDetalleModel> ColorDetalleByIDCrud(int idProducto);

        //Imagen
        List<CrudImagenModel> ImagenByIDCrud(int idProducto);
        bool InsertImagenes(ImagenModel oImagenModel);
        bool DeleteImagenes(int idImagenes);

        //ColorCrud
        List<ColorModel> GetAllColors();
        bool InsertColor(ColorModel colorModel);
        bool UpdateColor(ColorModel colorModel);
        bool DeleteColor(int idColor);


        //SizeCrud
        //List<SizeModel> SizesByIDCrud(int idProducto);
        List<SizeModel> GetAllSizes();
        bool DeleteSize(int idSize);
        bool UpdateSize(SizeModel oSizeModel);
        bool InsertSize(SizeModel oSizeModel);

        //ProvedorCrud
        //List<ProveedoresModel> GetProveedorByCorreo();
        List<ProveedoresModel> GetAllProveedores();
        bool InsertProveedor(ProveedoresModel proveedorModel);
        bool UpdateProveedor(ProveedoresModel proveedorModel);
        bool DeleteProveedor(int idProveedor);

    }
}
