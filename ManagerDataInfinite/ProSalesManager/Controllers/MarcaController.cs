using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Marca.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IB_Marca _Marca;
        public MarcaController(IB_Marca Marca)
        {
            _Marca = Marca;
        }
        [HttpGet]
        [Route("GetAllMarca")]
        public dynamic GetAllMarca()
        {
            var result = _Marca.ObtenerMarca(); // Asegúrate de cambiar el nombre del método al correcto

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Calidad" });

            return new
            {
                calidad = result
            };
        }

        [HttpGet]
        [Route("GetAllMarcaCombobox")]
        public dynamic GetAllMarcaCombobox()
        {
            var result = _Marca.ObtenerMarcasParaComboBox(); // Asegúrate de cambiar el nombre del método al correcto

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Calidad" });

            return new
            {
                result = result
            };
        }

        [HttpPost]
        [Route("PostMarca")]
        public dynamic InsertarMarca(string descripcion)
        {
            var result = _Marca.InsertarMarca(descripcion); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se insertó registro de Calidad" });

            return new
            {
                calidad = result
            };
        }

        [HttpPut]
        [Route("PutMarca")]
        public dynamic ActualizarMarca(int idCalidad, string descripcion)
        {
            var result = _Marca.ActualizarMarca(idCalidad, descripcion); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro de Calidad" });

            return new
            {
                calidad = result
            };
        }

        [HttpDelete]
        [Route("DeleteMarca")]
        public dynamic EliminarMarca(int idCalidad)
        {
            var result = _Marca.EliminarMarca(idCalidad); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se eliminó registro de Calidad" });

            return new
            {
                calidad = result
            };
        }
    }
}
