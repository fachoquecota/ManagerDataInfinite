using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProSalesManager._02_Busnisess.Transporte.Interfaces;

namespace ProSalesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaTransporteController : ControllerBase
    {
        private readonly IB_Transporte _Transporete;
        public EmpresaTransporteController(IB_Transporte Transporte)
        {
            _Transporete = Transporte;
        }
        [HttpGet]
        [Route("GetAllEmpresaTransporte")]
        public dynamic GetAllEmpresaTransporte()
        {
            var result = _Transporete.ObtenerEmpresaTransporte(); 

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de Calidad" });

            return new
            {
                result = result
            };
        }

        [HttpGet]
        [Route("GetAllTransporteCombobox")]
        public dynamic GetAllTransporteCombobox()
        {
            var result = _Transporete.ObtenerEmpresaTransporteComboBox(); 

            if (result is null)
                return BadRequest(new { message = "No se encontró datos de transporte" });

            return new
            {
                result = result
            };
        }

        [HttpPost]
        [Route("PostTransporte")]
        public dynamic InsertTransporte(string descripcion, string direccion)
        {
            var result = _Transporete.InsertarEmpresaTransporte(descripcion, direccion);

            if (result is false)
                return BadRequest(new { message = "No se insertó registro de transporte" });

            return new
            {
                result = result
            };
        }

        [HttpPut]
        [Route("PutTransporte")]
        public dynamic UpdateTransporte(int idEmpresaTranspte, string descripcion, string direccion)
        {
            var result = _Transporete.ActualizarEmpresaTransporte(idEmpresaTranspte, descripcion, direccion);

            if (result is false)
                return BadRequest(new { message = "No se actualizó registro de Transporte" });

            return new
            {
                result = result
            };
        }

        [HttpDelete]
        [Route("DeleteTransporte")]
        public dynamic DeleteTransporte(int idEmpresaTranspte)
        {
            var result = _Transporete.EliminarEmpresaTransporte(idEmpresaTranspte); 

            if (result is false)
                return BadRequest(new { message = "No se eliminó registro de Calidad" });

            return new
            {
                result = result
            };
        }
    }
}
