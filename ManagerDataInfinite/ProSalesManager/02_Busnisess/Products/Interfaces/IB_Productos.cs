using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Products.Interfaces
{
    public interface IB_Productos
    {
        List<ProductResponse> GetAllProductsDetails(bool Activo);
        //Crud
        List<CrudProductoModel> ProductosListaCrud();
        List<CrudProductoModel> ProductosByIDCrud(int idProducto);
        List<ComboBox> ProveedorCrudCB();
        List<ComboBox> GeneroCrudCB();
        List<ComboBox> CategoriaCrudCB();
        List<ComboBox> ModeloCrudCB();
        int UpdateProducto(CrudProductoModel oCrudProductoModel);
        bool DeleteProducto(int idProducto);
        bool InsertProducto(ProductoModel oProductoModel);

        //SizeDetalle
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
        bool InsertColorDetalleCrud(CrudColorDetalleModel crudColorDetalleModel);
        bool UpdateColorDetalleCrud(CrudColorDetalleModel crudColorDetalleModel);
        bool DeleteColorDetalleCrud(int id);

        //Imagenes
        List<CrudImagenModel> ImagenByIDCrud(int idProducto);
        bool InsertImagen(ImagenModel oImagenModel);
        bool DeleteImagenes(int idImagenes);

        //ColorCrud
        List<ColorModel> GetAllColors();
        bool InsertColor(ColorModel colorModel);
        bool UpdateColor(ColorModel colorModel);
        bool DeleteColor(int idColor);


        //SizeCrud
        List<SizeModel> GetAllSizes();
        bool DeleteSize(int idSize);
        bool UpdateSize(SizeModel sizeModel);
        bool InsertSize(SizeModel sizeModel);

        //ProvedorCrud
        List<ProveedoresModel> GetAllProveedores();
        bool InsertProveedor(ProveedoresModel proveedorModel);
        bool UpdateProveedor(ProveedoresModel proveedorModel);
        bool DeleteProveedor(int idProveedor);
    }
}
