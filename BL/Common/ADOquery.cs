using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Common
{
    public class ADOquery
    {
        public const string cadenaConexion = "Info_Ec_DB";

        public const string SP_VALIDAR_LOGIN = "EXEC VALIDAR_LOGIN @CedulaRecibida";

        public const string SP_OBTENER_TODOS_CLIENTES = "EXEC SP_Obtener_Todos_Clientes";
        public const string SP_REGISTRAR_CLIENTE = "EXEC SP_REGISTRAR_CLIENTE @Nombre, @Apellido, @Cedula, @Telefono, @Correo, @Direccion, @Edad";
        public const string SP_BUSCAR_CLIENTE_POR_CEDULA = "EXEC SP_Buscar_Cliente_PorCedula @Cedula";
        public const string SP_EDITAR_CLIENTE = "EXEC SP_Editar_Cliente @Nombre, @Apellido, @Cedula, @Telefono, @Correo, @Direccion, @Edad";
        public const string SP_ELIMINAR_CLIENTE_POR_CEDULA = "EXEC SP_Eliminar_Cliente_PorCedula @Cedula";


        public const string SP_OBTENER_TODOS_EMPLEADOS = "EXEC SP_Obtener_Todos_Empleados";
        public const string SP_REGISTRAR_EMPLEADO = "EXEC SP_REGISTRAR_EMPLEADO @Clave, @NombreRol, @Nombre, @Apellido, @Cedula, @Telefono, @Correo, @Direccion, @Edad;";
        public const string SP_BUSCAR_EMPLEADO_POR_CEDULA = "EXEC SP_BUSCAR_EMPLEADO_POR_CEDULA @Cedula";
        public const string SP_EDITAR_EMPLEADO = "EXEC SP_Editar_Empleado @NombreRol, @Nombre, @Apellido, @Cedula, @Telefono, @Correo, @Direccion, @Edad;";
        public const string SP_ELIMINAR_EMPLEADO_POR_CEDULA = "EXEC SP_ELIMINAR_EMPLEADO_POR_CEDULA @Cedula";
    }
}
