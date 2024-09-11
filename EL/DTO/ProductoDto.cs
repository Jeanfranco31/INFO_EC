using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.DTO
{
    public class ProductoDto
    {
        public int Id_Producto { get; set; }
        public string Nombre { get; set; } = null!;
        public string ImagenPath { get; set; } = null!;
        public decimal Precio { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public string NombreMarca { get; set; } = null!;
    }
}
