﻿using BL.Cliente;
using EL.DTO;
using EL.Response;
using Microsoft.AspNetCore.Mvc;

namespace INFO_EC_BACKEND.Controllers
{
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;
        private Response response = new();
        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        [Route(COMMON.Common.apiRegistrarCliente)]
        [HttpPost]
        public async Task<IActionResult> RegistrarCliente([FromBody] ClienteDTO cliente) {
            response = await _service.RegistrarCliente(cliente);
            if (response == null) {
                return BadRequest();
            }
            return Ok(response);
        }

        [Route(COMMON.Common.apiBuscarClientePorCedula)]
        [HttpGet]
        public async Task<IActionResult> BuscarClientePorCedula(string numeroCedula)
        {
            response = await _service.BuscarClientePorCedula(numeroCedula);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [Route(COMMON.Common.apiEliminarClientePorCedula)]
        [HttpDelete]
        public async Task<IActionResult> EliminarClientePorCedula(string numeroCedula)
        {
            response = await _service.EliminarClientePorCedula(numeroCedula);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }


    }
}
