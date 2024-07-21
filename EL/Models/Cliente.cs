using System;
using System.Collections.Generic;

namespace INFO_EC_BACKEND.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int? IdCuenta { get; set; }

    public virtual Cuentum? IdCuentaNavigation { get; set; }
}
