using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.DTO
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        public string? clave {  get; set; }
        public string? NombreRol {  get; set; }
        public string? NombreEmpleado { get; set; }
        public string? ApellidoEmpleado { get; set; }
        public string? CedulaEmpleado { get; set; }
        public string? TelefonoEmpleado { get; set; }
        public string? CorreoEmpleado { get; set; }
        public string? DireccionEmpleado { get; set; }
        public int EdadEmpleado { get; set; }
    }
}
