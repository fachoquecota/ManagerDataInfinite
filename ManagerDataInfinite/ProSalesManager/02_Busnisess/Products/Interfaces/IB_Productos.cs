﻿using ProSalesManager._03_Models;
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
        bool UpdateProducto(CrudProductoModel oCrudProductoModel);
        bool DeleteProducto(int idProducto);
        bool InsertProducto(ProductoModel oProductoModel);

        //SizeDetalle
        List<CrudSizeDetalleModel> SizeDetalleByIDCrud(int idProducto);
        List<ComboBox> SizeCrudCB();
        bool InsertSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel);
        bool UpdateSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel);
        bool DeleteSizeDetalle(CrudSizeDetalleModel oSizeDetalleModel);

        //Tags
        List<CrudTagDetalleModel> TagsByIDCrud(int idProducto);
        bool InsertTagCrud(CrudTagDetalleModel oCrudTagDetalleModel);
        bool UpdateTagCrud(CrudTagDetalleModel oCrudTagDetalleModel);
        bool DeleteTagCrud(CrudTagDetalleModel oCrudTagDetalleModel);

        //Color Detalle 
        List<ComboBox> ColorDetalleCrudCB();
        List<CrudColorDetalleModel> ColorDetalleByIDCrud(int idProducto);
    }
}
