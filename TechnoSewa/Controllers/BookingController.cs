using System.Security.Claims;
using Application.DTO.Booking;
using Application.Interfaces.Bookings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("create/booking/subcategory")]
        [Authorize]
        public async Task<IActionResult> PostProblem([FromBody] SubCategoryBookingDTO model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Enter valid data");
                }

                var result = await _bookingService.SubCategoryBooking(model, userId);
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
