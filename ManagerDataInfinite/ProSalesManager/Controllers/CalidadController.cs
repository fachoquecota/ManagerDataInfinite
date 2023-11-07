using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Calidad.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalidadController : ControllerBase
    {
        private readonly IB_Calidad _Calidad;
        public CalidadController(IB_Calidad Calidad)
        {
            _Calidad = Calidad;
        }
        [HttpGet]
        [Route("GetAllCalidad")]
        public dynamic GetAllCalidad()
        {
            var result = _Calidad.ObtenerCalidad(); // Asegúrate de cambiar el nombre del método al correcto

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Calidad" });

            return new
            {
                calidad = result
            };
        }

        [HttpPost]
        [Route("PostCalidad")]
        public dynamic InsertCalidad(string descripcion)
        {
            var result = _Calidad.InsertCalidad(descripcion); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se insertó registro de Calidad" });

            return new
            {
                calidad = result
            };
        }

        [HttpPut]
        [Route("PutCalidad")]
        public dynamic UpdateCalidad(int idCalidad, string descripcion)
        {
            var result = _Calidad.UpdateCalidad(idCalidad, descripcion); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro de Calidad" });

            return new
            {
                calidad = result
            };
        }

        [HttpDelete]
        [Route("DeleteCalidad")]
        public dynamic DeleteCalidad(int idCalidad)
        {
            var result = _Calidad.DeleteCalidad(idCalidad); // Asegúrate de cambiar el nombre del método al correcto

            if (result is false)
                return BadRequest(new { message = "No se eliminó registro de Calidad" });

            return new
            {
                calidad = result
            };
        }

    }
}
