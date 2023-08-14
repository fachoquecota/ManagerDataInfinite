﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IB_Productos _Products;
        private readonly ITokenService _tokenService;
        public ProductosController(IB_Productos Products, ITokenService tokenService)
        {
            _Products = Products;
            _tokenService = tokenService;

        }
        [HttpGet]
        [Route("GetProductos")]
        public dynamic GetProducts()
        {
            var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.Productos(correo);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Productos" });

            return new
            {
                //message = jwtToken
                message = result
            };
        }
        [HttpPut]
        [Route("PutProductos")]
        public dynamic PutProducts([FromBody]ProductoModel oProductoModel)
        {
            bool result = _Products.UpdateProductos(oProductoModel);
            return Ok(result);
        }
        [HttpPost]
        [Route("PostProductos")]
        public dynamic PostProducts([FromBody] ProductoModel oProductoModel)
        {
            bool result = _Products.InsertProducto(oProductoModel);
            return Ok(result);
        }
    }
}
