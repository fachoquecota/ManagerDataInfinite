using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Categoria.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IB_Categoria _Marca;
        public CategoriaController(IB_Categoria Categoria)
        {
            _Marca = Categoria;
        }
        [HttpGet]
        [Route("GetAllCategoria")]
        public dynamic GetAllCategoria()
        {
            var result = _Marca.ObtenerCategoria(); // Asegúrate de cambiar el nombre del método al correcto

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Calidad" });

            return new
            {
                result = result
            };
        }


        [HttpPost]
        [Route("PostCategoria")]
        public dynamic InsertarCategoria(string descripcion)
        {
            var result = _Marca.InsertarCategoria(descripcion); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se insertó registro de Calidad" });

            return new
            {
                result = result
            };
        }

        [HttpPut]
        [Route("PutCategoria")]
        public dynamic ActualizarCategoria(int idCategoria, string descripcion)
        {
            var result = _Marca.ActualizarCategoria(idCategoria, descripcion); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro de Calidad" });

            return new
            {
                result = result
            };
        }

        [HttpDelete]
        [Route("DeleteCategoria")]
        public dynamic EliminarCategoria(int idCategoria)
        {
            var result = _Marca.EliminarCategoria(idCategoria); 


            if (result is false)
                return BadRequest(new { message = "No se eliminó registro de categoria" });

            return new
            {
                result = result
            };
        }
    }
}
