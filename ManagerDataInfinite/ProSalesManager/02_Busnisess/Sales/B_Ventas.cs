using ProSalesManager._01_Data.Modules.Sales.Interfaces;
using ProSalesManager._02_Busnisess.Sales.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Sales
{
    public class B_Ventas : IB_Ventas
    {
        private readonly ISP_Ventas _sP_Ventas;
        public B_Ventas(ISP_Ventas sP_Ventas)
        {
            _sP_Ventas = sP_Ventas;
        }
        public List<ComboBox> TipoVentaCB()
        {
            var tipoVentasCB = _sP_Ventas.TipoVentaCB();
            return tipoVentasCB;
        }
        public List<ProductoVenta> ObtenerProductosVenta()
        {
            var tipoVentasCB = _sP_Ventas.ObtenerProductosVenta();
            return tipoVentasCB;
        }

        public bool InsertarVentaConDetalles(Venta venta)
        {
            var Venta = _sP_Ventas.InsertarVentaConDetalles(venta);
            return Venta;
        }

        public List<VentasModel> ObtenerVentas()
        {
            var venta = _sP_Ventas.ObtenerVentas();
            return venta;
        }

        public List<VentasModel> ObtenerVentasFiltro(FiltroVentasModel filtro)
        {
            var venta = _sP_Ventas.ObtenerVentasFiltro(filtro);
            return venta;
        }

        public List<UbigeoModel> ObtenerUbigeoVenta()
        {
            var venta = _sP_Ventas.ObtenerUbigeoVenta();
            return venta;
        }
        public List<ReporteDetalle> ObtenerRptDetalle(int idVenta)
        {
            var venta = _sP_Ventas.ObtenerRptDetalle(idVenta);
            return venta;
        }
    }
}
