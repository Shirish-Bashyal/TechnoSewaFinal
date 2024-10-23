using Application.DTO.User.Auth;
using Application.Interfaces.User.Auth;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("signIn")]
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

        [HttpPost]
        [Route("sendOtp")]
        public async Task<IActionResult> SendOtp([FromBody] string phoneNumber)
        {
            //generate a otp
            //save the number and otp in cache
            //send otp to the user via sms
            return Ok("Sms Sent"); //otp is 1234
        }

        [HttpPost]
        [Route("verifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerifyDTO model)
        {
            //verify the otp with phone number
            return Ok("verified");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model not valid");
            }
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("password doesnot match confirm Password");
            }

            var result = await _authService.Register(model);

            if (result.Success)
            {
                return StatusCode(StatusCodes.Status201Created, result.Message);
            }
            else
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, result.Message);
            }
        }

        [HttpPost]
        [Route("signOut")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true, // Only transmit over HTTPS in production
                SameSite = SameSiteMode.Strict
            };

            // Remove the user's JWT cookie
            Response.Cookies.Append("MyAuthValue", string.Empty, cookieOptions);

            return Ok();
        }
    }
}
