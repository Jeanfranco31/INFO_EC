using EL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.MenuOpciones
{
    public interface IMenuOpcion
    {
        Task<Response> getAllOptions();
    }
}
