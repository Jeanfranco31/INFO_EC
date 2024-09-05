using BL.Auth.Login;
using EL.DTO;
using EL.Response;
using Microsoft.AspNetCore.Mvc;

namespace INFO_EC_BACKEND.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private LoginService service;
        private Response response = new();
        public LoginController(LoginService _service)
        {
            service = _service;
        }

        [Route(COMMON.Common.apiValidarLogin)]
        [HttpPost]
        public async Task<IActionResult> validateLogin([FromBody]LoginDTO loginData)
        {
            response = await service.validateLogin(loginData);
            return Ok(response);
        }
    }
}
