using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._02_Busnisess.Sales.Interfaces
{
    public interface IB_Ventas
    {
        List<ComboBox> TipoVentaCB();
        List<ProductoVenta> ObtenerProductosVenta();
        bool InsertarVentaConDetalles(Venta venta);
    }
}
