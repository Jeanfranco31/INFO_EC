using BL.MenuOpciones;
using EL.Response;
using Microsoft.AspNetCore.Mvc;

namespace INFO_EC_BACKEND.Controllers
{
    public class MenuOpcionController : Controller
    {
        private MenuOpcionService service;
        private Response response = new();
        public MenuOpcionController(MenuOpcionService _service) 
        {
            service = _service;
        }
       
        [HttpGet(COMMON.Common.apiObtenerMenuOpciones)]
        public async Task<IActionResult> getMenuOpciones() {
            response = await service.getAllOptions();
            return Ok(response);
        }
    }

}
