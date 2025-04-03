using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Application.DTO.User.Role;
using Application.Interfaces.User.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO model)
        {
            if (string.IsNullOrWhiteSpace(model.RoleName))
            {
                return BadRequest("RoleName is required.");
            }

            var result = await _roleServices.CreateRole(model.RoleName);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("ChangeRole")]
        [Authorize]
        public async Task<IActionResult> ChangeRole(string role)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                return BadRequest("RoleName is required.");
            }

            var result = await _roleServices.ChangeRole(userId, role);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
