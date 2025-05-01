using System.Security.Claims;
using Application.DTO.Technician;
using Application.DTO.User.Post;
using Application.Interfaces.Technician;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianService _technicianService;

        public TechnicianController(ITechnicianService technicianService)
        {
            _technicianService = technicianService;
        }

        [HttpPost]
        [Route("create/technician")]
        [Authorize]
        public async Task<IActionResult> BecomeTechnician([FromBody] BecomeTechnicianDTO model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Enter valid data");
                }

                var result = await _technicianService.BecomeTechnician(model, userId);
                if (result.Success)
                {
                    return StatusCode(201, result);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
