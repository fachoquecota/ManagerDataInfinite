using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager._01_Data.Modules.Sales.Interfaces
{
    public interface ISP_Ventas
    {
        List<ComboBox> TipoVentaCB();
        List<ProductoVenta> ObtenerProductosVenta();
    }
}
