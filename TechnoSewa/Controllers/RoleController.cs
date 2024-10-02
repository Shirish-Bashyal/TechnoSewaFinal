using System.ComponentModel.DataAnnotations;
using Application.DTO.User.Role;
using Application.Interfaces.User.Role;
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
    }
}
