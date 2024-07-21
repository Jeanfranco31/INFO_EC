using System;
using EL.Response;
using EL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cliente
{
    public interface ICliente
    {
        Task<Response> RegistrarCliente(ClienteDTO cliente);
        Task<Response> BuscarClientePorCedula(string numeroCedula);
        Task<Response> ActualizarClientePorCedula(ClienteDTO cliente);
        Task<Response> EliminarClientePorCedula(string numeroCedula);
    }
}
