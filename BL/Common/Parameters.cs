using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Common
{
    public class Parameters
    {
        #region LOGIN PARAMETERS
        public const string cedula = "@CedulaRecibida";
        #endregion

        #region Parametros Tabla CLIENTE
        public const string nombreCliente = "@Nombre";
        public const string apellidoCliente = "@Apellido";
        public const string cedulaCliente = "@Cedula";
        public const string correoCliente = "@Correo";
        public const string direccionCliente = "@Direccion";
        public const string telefonoCliente = "@Telefono";
        public const string edadCliente = "@Edad";
        #endregion

        #region Parametros Tabla EMPLEADO

        public const string claveEmpleado = "@Clave";
        public const string nombreRol = "@NombreRol";
        public const string nombreEmpleado = "@Nombre";
        public const string apellidoEmpleado = "@Apellido";
        public const string cedulaEmpleado = "@Cedula";
        public const string telefonoEmpleado = "@Telefono";
        public const string correoEmpleado = "@Correo";
        public const string direccionEmpleado = "@Direccion";
        public const string edadEmpleado = "@Edad";

        #endregion

        #region Tabla Producto

        public const string param_IdProducto = "@Id_Producto";
        public const string param_NombreProducto = "@Nombre";
        public const string paramImagenPath = "@ImagenPath";
        public const string paramPrecio = "@Precio";
        public const string paramNombreCategoria = "@NombreCategoria";
        public const string paramNombreMarca = "@NombreMarca";

        #endregion
    }
}
