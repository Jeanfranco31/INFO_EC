using EL.DTO;
using EL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Empleado
{
    public interface IEmpleado
    {
        Task<Response> ObtenerEmpleados();
        Task<Response> ObtenerEmpleadoPorCedula(string numeroCedula);
        Task<Response> RegistrarEmpleado(EmpleadoDTO empleado);
        Task<Response> EditarDatosEmpleado(EmpleadoDTO empleado);
        Task<Response> EliminarEmpleado(string numeroCedula);

    }
}
