using Application.DTO.User.Auth;
using Application.Interfaces.User.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.SignIn(model);
            if (result.Success)
            {
                var cookieOptions = new CookieOptions { HttpOnly = true, Secure = true, };

                Response.Cookies.Append("MyAuthValue", result.Data.ToString(), cookieOptions);
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
