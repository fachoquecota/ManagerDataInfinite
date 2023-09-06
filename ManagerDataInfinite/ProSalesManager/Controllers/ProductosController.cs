﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Products.Interfaces;
using ProSalesManager._03_Models;
using ProSalesManager._03_Models.ModelsCrud;
using ProSalesManager._04_Services.Login.Interfaces;

namespace ProSalesManager.Controllers
{
    //[Authorize]
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
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.GetAllProductsDetails(true);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Productos" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpGet]
        [Route("GetCrudProductos")]
        public dynamic GetCrudProductos()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ProductosListaCrud();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Productos" });

            return new
            {
                //message = jwtToken
                products = result
            };
        }
        [HttpGet]
        [Route("GetCrudProductoById")]
        public dynamic GetCrudProductoById(int idProducto)
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ProductosByIDCrud(idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del Producto" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetCrudProveedorCrudCB")]
        public dynamic GetCrudProveedorCrudCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ProveedorCrudCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del Proveedor" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetCrudGeneroCrudCB")]
        public dynamic GetCrudGeneroCrudCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.GeneroCrudCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del Genero" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetCrudCategoriaCrudCB")]
        public dynamic GetCrudCategoriaCrudCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.CategoriaCrudCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de categoria" });

            return new
            {
                result = result
            };
        }
        [HttpPut]
        [Route("PutCrudProducto")]
        public dynamic PutCrudProducto(CrudProductoModel oCrudProductoModel)
        {
            var result = _Products.UpdateProducto(oCrudProductoModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó" });

            return new
            {
                result = result,
                message = "Actualizado"
            };
        }
        [HttpDelete]
        [Route("DeleteCrudProducto")]
        public dynamic DeleteCrudProducto(int idProducto)
        {
            var result = _Products.DeleteProducto(idProducto);

            if (result is false)
                return BadRequest(new { message = "Se eliminó" });

            return new
            {
                result = result,
                message = "Eliminado"
            };
        }

        //SizeDetalle
        [HttpGet]
        [Route("GetCrudSizeDetalleById")]
        public dynamic GetCrudSizeDetalleById(int idProducto)
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.SizeDetalleByIDCrud(idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del Size Detalle" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetSize_CrudCB")]
        public dynamic GetSize_CrudCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.SizeCrudCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del Genero" });

            return new
            {
                result = result
            };
        }
        [HttpPut]
        [Route("PutCrudSizeDetalle")]
        public dynamic PutCrudProducto(CrudSizeDetalleModel oCrudSizeDetalleModel)
        {
            var result = _Products.UpdateSizeDetalle(oCrudSizeDetalleModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó" });

            return new
            {
                result = result,
                message = "Actualizado"
            };
        }
        [HttpPost]
        [Route("PostCrudSizeDetalle")]
        public dynamic PostCrudSizeDetalle(CrudSizeDetalleModel oCrudSizeDetalleModel)
        {
            var result = _Products.InsertSizeDetalle(oCrudSizeDetalleModel);

            if (result is false)
                return BadRequest(new { message = "Se insertó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }
        [HttpDelete]
        [Route("DeleteCrudSizeDetalle")]
        public dynamic DeleteCrudSizeDetalle(CrudSizeDetalleModel oCrudSizeDetalleModel)
        {
            var result = _Products.DeleteSizeDetalle(oCrudSizeDetalleModel);

            if (result is false)
                return BadRequest(new { message = "Se eliminó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }
    }
}
