using System.Security.Claims;
using Application.DTO.Technician;
using Application.Interfaces.Technician;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpPost]
        [Route("create/Bid")]
        [Authorize]
        public async Task<IActionResult> CreateBid([FromBody] CreateBidDTO model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Enter valid data");
                }

                var result = await _bidService.CreateBid(model, userId);
                if (result.Success)
                {
                    return StatusCode(200, result);
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

        [HttpGet]
        [Route("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllForPost(int postId)
        {
            var result = await _bidService.GetAllForPost(postId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        [HttpGet]
        [Route("get/bids/technician")]
        [Authorize]
        public async Task<IActionResult> GetAllForTechnician()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var result = await _bidService.GetAllForTechnician(userId);
                if (result.Success)
                {
                    return Ok(result);
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
