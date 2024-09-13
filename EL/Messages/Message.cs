using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Messages
{
    public class Message
    {
        public const string loginSuccess = "Bienvenido";
        public const string loginError = "Datos incorrectos";

        public const string clienteObtenido = "Datos obtenidos";
        public const string clienteRegistrado = "Cliente registrado correctamente";
        public const string clienteNoRegistrado = "No se pudo registrar el cliente.";
        public const string clienteExiste = "Cliente encontrado.";
        public const string clienteNoExiste = "No existe un cliente asociado al numero de cedula ingresado";
        public const string clienteEditado = "Se han modificado los datos correctamente";
        public const string clienteNoEditado = "No se pudo editar los datos del cliente";
        public const string clienteEliminado = "Cliente eliminado satisfactoriamente.";

        public const string empleadoRegistrado = "Datos registrados del empleado.";
        public const string empleadoNoRegistrado = "No se pudo registrar los datos.";
        public const string empleadoEncontrado = "Datos del empleado encontrados";
        public const string empleadoNoEncontrado = "Datos del empleado no encontrados";
        public const string empleadoEliminado = "Datos del empleado eliminados";
        public const string empleadoNoEliminado = "No se pudo eliminar al empleado.";
        public const string empleadoObtenido = "Lista de Empleados";
        public const string empleadoEditado = "Informacion del empleado actualizada";
        public const string empleadoNoEditado = "Al parecer no existe un empleado con el numero de cedula ingresado";

        public const string productoObtenido = "Informacion de productos";
        public const string productExists = "Producto Obtenido";
        public const string productNotExists = "Al parecer no existe un producto con el ID seleccionado";
    }
}
