using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Sales.Interfaces
{
    public interface ISP_Ventas
    {
        List<ComboBox> TipoVentaCB();
        List<ProductoVenta> ObtenerProductosVenta();
        bool InsertarVentaConDetalles(Venta venta);
        List<VentasModel> ObtenerVentas();
        List<UbigeoModel> ObtenerUbigeoVenta();
        List<VentasModel> ObtenerVentasFiltro(FiltroVentasModel filtro);
        List<ReporteDetalle> ObtenerRptDetalle(int idVenta);
    }
}
