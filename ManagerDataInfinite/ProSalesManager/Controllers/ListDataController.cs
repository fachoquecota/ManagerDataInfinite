using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListDataController : ControllerBase
    {
        private readonly IB_Productos _Products;
        private readonly ITokenService _tokenService;
        public ListDataController(IB_Productos Products, ITokenService tokenService)
        {
            _Products = Products;
            _tokenService = tokenService;

        }
        //[HttpGet]
        //[Route("ListProductos")]
        //public dynamic GetProducts()
        //{
        //    var result = _Products.Productos(correo);

        //    if (result is null)
        //        return BadRequest(new { message = "No se encontró datos de Productos" });

        //    return new
        //    {
        //        //message = jwtToken
        //        message = result
        //    };
        //}
    }
}