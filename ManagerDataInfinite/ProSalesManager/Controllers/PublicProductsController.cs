using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.PublicProducts.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicProductsController : ControllerBase
    {
        private readonly IB_PublicProducts _Products;
        public PublicProductsController(IB_PublicProducts Products)
        {
            _Products = Products;
        }
        [HttpGet]
        [Route("GetProductos")]
        public dynamic GetProducts(int idProducto)
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ProductsData(true, idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Productos" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
    }
}
