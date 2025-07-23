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

        private readonly IConsumerService _consumer;

        public ConsumerController(IProfileService profile, IConsumerService consumer)
        {
            _profile = profile;
            _consumer = consumer;
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

        [HttpGet("all")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            var consumers = await _consumer.GetAll();
            if (consumers.Success)
                return Ok(consumers);
            else
                return NotFound();
        }
    }
}
