using BL.Empleado;
using EL.DTO;
using EL.Response;
using Microsoft.AspNetCore.Mvc;

namespace INFO_EC_BACKEND.Controllers
{
    public class EmpleadoController : Controller
    {
        private Response response = new();
        private EmpleadoService _service;
        public EmpleadoController(EmpleadoService service)
        {
            _service = service;
        }

        [Route(COMMON.Common.apiObtenerEmpleados)]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosEmpleados()
        {
            response = await _service.ObtenerEmpleados();
            return Ok(response);
        }

        [Route(COMMON.Common.apiBuscarEmpleadoPorCedula)]
        [HttpGet]
        public async Task<IActionResult> BuscarEmpleadoPorCedula(string cedulaABuscar) {
            response = await _service.ObtenerEmpleadoPorCedula(cedulaABuscar);
            return Ok(response);
        }

        [Route(COMMON.Common.apiRegistrarEmpleado)]
        [HttpPost]
        public async Task<IActionResult> RegistrarEmpleado([FromBody] EmpleadoDTO empleadoDTO)
        {
            response = await _service.RegistrarEmpleado(empleadoDTO);
            return Ok(response);
        }

        [Route(COMMON.Common.apiEditarEmpleado)]
        [HttpPut]
        public async Task<IActionResult> EditarEmpleado([FromBody] EmpleadoDTO empleadoDTO)
        {
            response = await _service.EditarDatosEmpleado(empleadoDTO);
            return Ok(response);
        }

        [Route(COMMON.Common.apiEliminarEmpleado)]
        [HttpDelete]
        public async Task<IActionResult> EliminarEmpleado(string cedulaAEliminar)
        {
            response = await _service.EliminarEmpleado(cedulaAEliminar);
            return Ok(response);
        }

    }
}
