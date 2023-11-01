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
    }
}
