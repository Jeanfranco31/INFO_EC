using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Messages
{
    public class Message
    {
        public const string clienteRegistrado = "Cliente registrado correctamente";
        public const string clienteNoRegistrado = "No se pudo registrar el cliente.";
        public const string clienteExiste = "Cliente encontrado.";
        public const string clienteNoExiste = "No existe un cliente asociado al numero de cedula ingresado";
    }
}
