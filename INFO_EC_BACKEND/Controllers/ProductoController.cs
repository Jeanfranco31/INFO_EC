using BL.Producto;
using EL.Response;
using Microsoft.AspNetCore.Mvc;

namespace INFO_EC_BACKEND.Controllers
{
    [ApiController]
    public class ProductoController : Controller
    {
        private ProductService _productService;
        private Response response  = new();

        public ProductoController(ProductService service) => _productService = service;

        [HttpGet(COMMON.Common.apiObtenerProductos)]
        public async Task<IActionResult> GetAll()
        {
            response = await _productService.getAllProducts();
            return Ok(response);
        }

        [HttpGet(COMMON.Common.apiObtenerProductoById)]
        public async Task<IActionResult> GetProductById(int id)
        {
            response = await _productService.getProductById(id);
            return Ok(response);
        }
    }
}
