using System.Security.Claims;
using Application.Interfaces.User.Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IProfileService _profile;

        public ConsumerController(IProfileService profile)
        {
            _profile = profile;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetConsumerProfile()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var profile = await _profile.ConsumerProfile(userId);
                return Ok(profile);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
