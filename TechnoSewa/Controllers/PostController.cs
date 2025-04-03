using System.Security.Claims;
using Application.DTO.User.Post;
using Application.Interfaces.User.Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _post;

        public PostController(IPostService post)
        {
            _post = post;
        }

        [HttpPost]
        [Route("problem")]
        [Authorize]
        public async Task<IActionResult> PostProblem([FromForm] CreatePostDTO model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Enter valid data");
                }

                var result = await _post.CreatePost(model, userId);
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

        [HttpGet]
        [Route("get")]
        [Authorize]
        public async Task<IActionResult> Get(int postId)
        {
            var result = await _post.GetPost(postId);
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
        [Route("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var result = await _post.GetAllPosts(userId);
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
