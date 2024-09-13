using EL.DTO;
using EL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Producto
{
    public interface IProducto
    {
        Task<Response> getAllProducts();
        Task<Response> getProductById(int id);
        Task<Response> addProduct(ProductoDto product);
        Task<Response> updateProduct(ProductoDto product);
        Task<Response> removeProduct(int id);
    }
}
