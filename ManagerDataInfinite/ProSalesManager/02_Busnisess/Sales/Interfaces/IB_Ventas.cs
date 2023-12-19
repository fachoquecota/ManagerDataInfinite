using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Sales.Interfaces
{
    public interface IB_Ventas
    {
        List<ComboBox> TipoVentaCB();
        List<ProductoVenta> ObtenerProductosVenta();
        bool InsertarVentaConDetalles(Venta venta);
        List<VentasModel> ObtenerVentas();
        List<UbigeoModel> ObtenerUbigeoVenta();
        List<VentasModel> ObtenerVentasFiltro(FiltroVentasModel filtro);
        List<ReporteDetalle> ObtenerRptDetalle(int idVenta);
        List<ReporteVentaGraficoModel> ReporteVentaGrafico();
        List<VentaDetalleModel> ReporteVentaDetalleGrafico();
    }
}
