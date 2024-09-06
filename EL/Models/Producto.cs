using System;
using System.Collections.Generic;

namespace INFO_EC_BACKEND.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public decimal Precio { get; set; }

    public int? CategoriaIdCategoria { get; set; }

    public virtual Categoria? CategoriaIdCategoriaNavigation { get; set; }
}
