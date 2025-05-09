using System.Security.Claims;
using Application.DTO.Review;
using Application.DTO.User.Post;
using Application.Interfaces.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Enter valid data");
            }

            var result = await _reviewServices.CreateReview(model);
            if (result.Success)
            {
                return StatusCode(201, result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
