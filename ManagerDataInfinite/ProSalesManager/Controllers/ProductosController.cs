using Microsoft.AspNetCore.Authorization;
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
        [Route("details")]
        public IActionResult GetProducts(int productId) // Cambié el tipo de retorno a IActionResult para seguir las mejores prácticas de ASP.NET Core.
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);

            var result = _Products.GetProductDetails(true, productId);

            if (result is null)
            {
                return BadRequest(new { message = "No se encontró datos de Productos" });
            }

            return Ok(new { product = result }); // Cambié 'products' a 'product' y quité los corchetes.
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
        [HttpGet]
        [Route("GetModeloDetalle")]
        public dynamic GetModeloDetalle()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ModeloProductosDetalleLista();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de modelo detalle" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetCrudModeloCrudCB")]
        public dynamic GetCrudModeloCrudCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ModeloCrudCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de modelo" });

            return new
            {
                result = result
            };
        }
        [HttpPut]
        [Route("PutCrudProducto")]
        public dynamic PutCrudProducto(CrudProductoModel oCrudProductoModel)
        {
            int result = _Products.UpdateProducto(oCrudProductoModel);

            if (result == -1)
                return BadRequest(new { message = "No se actualizó" });
            string message = string.Empty;
            if (result > 0 && oCrudProductoModel.idProducto == 0)
            {
                message = "Insertado";
            }
            else
            {
                if (result > 0 && oCrudProductoModel.idProducto != 0)
                {
                    message = "Actualizado";
                }
            }
            return Ok(result);
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
        public dynamic DeleteCrudSizeDetalle(int idSizeDetalle)
        {
            var result = _Products.DeleteSizeDetalle(idSizeDetalle);

            if (result is false)
                return BadRequest(new { message = "Se eliminó" });

            return new
            {
                result = result,
                message = "Eliminado"
            };
        }

        //TagDetalle
        [HttpGet]
        [Route("GetCrudTagById")]
        public dynamic GetCrudTagById(int idProducto)
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.TagsByIDCrud(idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de tag" });

            return new
            {
                result = result
            };
        }
        [HttpPut]
        [Route("PutCrudTag")]
        public dynamic PutCrudTag(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            var result = _Products.UpdateTagCrud(oCrudTagDetalleModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó" });

            return new
            {
                result = result,
                message = "Actualizado"
            };
        }
        [HttpPost]
        [Route("PostCrudTag")]
        public dynamic PostCrudTag(CrudTagDetalleModel oCrudTagDetalleModel)
        {
            var result = _Products.InsertTagCrud(oCrudTagDetalleModel);

            if (result is false)
                return BadRequest(new { message = "Se insertó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }
        [HttpDelete]
        [Route("DeleteCrudTag")]
        public dynamic DeleteCrudTag(int id)
        {
            var result = _Products.DeleteTagCrud(id);

            if (result is false)
                return BadRequest(new { message = "Se eliminó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }

        //ColorDetalle
        [HttpGet]
        [Route("GetColor_CrudCB")]
        public dynamic GetColor_CrudCB()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ColorDetalleCrudCB();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del color" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GetCrudColorDetalleById")]
        public dynamic GetCrudColorDetalleById(int idProducto)
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ColorDetalleByIDCrud(idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Color" });

            return new
            {
                result = result
            };
        }
        [HttpPost]
        [Route("PostCrudColorDetalle")]
        public dynamic PostCrudColorDetalle(CrudColorDetalleModel crudColorDetalleModel)
        {
            var result = _Products.InsertColorDetalleCrud(crudColorDetalleModel);

            if (result is false)
                return BadRequest(new { message = "Se insertó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }
        [HttpPut]
        [Route("PutCrudColorDetalle")]
        public dynamic PutCrudColorDetalle(CrudColorDetalleModel crudColorDetalleModel)
        {
            var result = _Products.UpdateColorDetalleCrud(crudColorDetalleModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó" });

            return new
            {
                result = result,
                message = "Actualizado"
            };
        }
        [HttpDelete]
        [Route("DeleteCrudColorDetalle")]
        public dynamic DeleteCrudColorDetalle(int id)
        {
            var result = _Products.DeleteColorDetalleCrud(id);

            if (result is false)
                return BadRequest(new { message = "Se eliminó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }


        //ColorDetalle
        [HttpGet]
        [Route("GETModeloProductosListaCrud")]
        public dynamic ModeloProductosListaCrud()
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ModeloProductosListaCrud();

            if (result is null)
                return BadRequest(new { message = "No se encontró datos del color" });

            return new
            {
                result = result
            };
        }
        [HttpGet]
        [Route("GETModeloProductosByIDCrud")]
        public dynamic ModeloProductosByIDCrud(int idProducto)
        {
            //var correoToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            //var correo = _tokenService.GetCorreoFromToken(correoToken);
            var result = _Products.ModeloProductosByIDCrud(idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Color" });

            return new
            {
                result = result
            };
        }
        [HttpPost]
        [Route("POSTInsertModeloProducto")]
        public dynamic InsertModeloProducto(ModeloProductoModel oModeloProductoModel)
        {
            var result = _Products.InsertModeloProducto(oModeloProductoModel);

            if (result is false)
                return BadRequest(new { message = "Se insertó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }
        [HttpPut]
        [Route("PUTUpdateModeloProducto")]
        public dynamic UpdateModeloProducto(ModeloProductoModel oModeloProductoModel)
        {
            var result = _Products.UpdateModeloProducto(oModeloProductoModel);

            if (result is false)
                return BadRequest(new { message = "No se actualizó" });

            return new
            {
                result = result,
                message = "Actualizado"
            };
        }
        [HttpDelete]
        [Route("DELETEDeleteModeloProducto")]
        public dynamic DeleteModeloProducto(int idModeloProducto)
        {
            var result = _Products.DeleteModeloProducto(idModeloProducto);

            if (result is false)
                return BadRequest(new { message = "Se eliminó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }



        //Imagenes
        [HttpGet]
        [Route("GetCrudImagenById")]
        public dynamic GetCrudImagenById(int idProducto)
        {
            var result = _Products.ImagenByIDCrud(idProducto);

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de imagen" });

            return new
            {
                result = result
            };
        }

        [HttpPost]
        [Route("PostCrudImagen")]
        public dynamic PostCrudImagen(ImagenModel oImagenModel)
        {
            var result = _Products.InsertImagen(oImagenModel);

            if (result is false)
                return BadRequest(new { message = "Se insertó" });

            return new
            {
                result = result,
                message = "insertado"
            };
        }

        [HttpDelete]
        [Route("DeleteImagen")]
        public dynamic DeleteCrudImagen(int oImagenModel)
        {
            var result = _Products.DeleteImagenes(oImagenModel);

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
