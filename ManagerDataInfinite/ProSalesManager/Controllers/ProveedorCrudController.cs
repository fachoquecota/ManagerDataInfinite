using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorCrudController : ControllerBase
    {
        private readonly IB_Productos _Products;
        public ProveedorCrudController(IB_Productos Products)
        {
            _Products = Products;
        }
        [HttpGet]
        [Route("GetAllProveedores")]
        public dynamic GetAllProveedores()
        {
            var result = _Products.GetAllProveedores();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Productos" });

            return new
            {
                //message = jwtToken
                products = result
            };


        }
        [HttpPost]
        [Route("PostProveedor")]
        public dynamic InsertProveedor(ProveedoresModel proveedorModel)
        {
            var result = _Products.InsertProveedor(proveedorModel);

            if (result is false)
                return BadRequest(new { message = "No se encontró insertó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpPut]
        [Route("PutProveedor")]
        public dynamic UpdateProveedor(ProveedoresModel proveedorModel)
        {
            var result = _Products.UpdateProveedor(proveedorModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpDelete]
        [Route("DeleteProveedor")]
        public dynamic DeleteProveedor(int idProveedor)
        {
            var result = _Products.DeleteProveedor(idProveedor);

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
    }
}
