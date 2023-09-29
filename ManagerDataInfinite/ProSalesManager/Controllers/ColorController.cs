using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IB_Productos _Products;
        public ColorController(IB_Productos Products)
        {
            _Products = Products;
        }
        [HttpGet]
        [Route("GetAllColors")]
        public dynamic GetAllColors()
        {
            var result = _Products.GetAllColors();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Productos" });

            return new
            {
                //message = jwtToken
                products = result
            };


        }
        [HttpPost]
        [Route("PostColor")]
        public dynamic InsertColor(ColorModel oColorModel)
        {
            var result = _Products.InsertColor(oColorModel);

            if (result is false)
                return BadRequest(new { message = "No se encontró insertó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpPut]
        [Route("PutColor")]
        public dynamic UpdateColor(ColorModel oColorModel)
        {
            var result = _Products.UpdateColor(oColorModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpDelete]
        [Route("DeleteColor")]
        public dynamic DeleteColor(int idColor)
        {
            var result = _Products.DeleteColor(idColor);

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
