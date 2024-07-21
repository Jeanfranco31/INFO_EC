using System;
using System.Collections.Generic;

namespace INFO_EC_BACKEND.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Clave { get; set; } = null!;

    public int? RolIdRol { get; set; }

    public int? CuentaIdCuenta { get; set; }

    public virtual Cuentum? CuentaIdCuentaNavigation { get; set; }

    public virtual Rol? RolIdRolNavigation { get; set; }
}
