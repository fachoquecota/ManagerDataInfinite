using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models.ModelsCrud;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly IB_Productos _Products;
        public SizeController(IB_Productos Products)
        {
            _Products = Products;
        }
        [HttpGet]
        [Route("GetAllSizes")]
        public dynamic GetAllSizes()
        {
            var result = _Products.GetAllSizes();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Size" });

            return new
            {
                //message = jwtToken
                products = result
            };


        }
        [HttpPost]
        [Route("PostSize")]
        public dynamic InsertSize(SizeModel oSizeModel)
        {
            var result = _Products.InsertSize(oSizeModel);

            if (result is false)
                return BadRequest(new { message = "No se encontró insertó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpPut]
        [Route("PutSize")]
        public dynamic UpdateSize(SizeModel oSizeModel)
        {
            var result = _Products.UpdateSize(oSizeModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpDelete]
        [Route("DeleteSize")]
        public dynamic DeleteSize(int idSize)
        {
            var result = _Products.DeleteSize(idSize);

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
