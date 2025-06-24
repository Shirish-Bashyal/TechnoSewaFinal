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
        [Route("create/subcategoryBooking")]
        [Authorize]
        public async Task<IActionResult> SubCategoryBooking([FromBody] SubCategoryBookingDTO model)
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

        [HttpPost]
        [Route("create/bidBooking")]
        [Authorize]
        public async Task<IActionResult> BidBooking(int bidId)
        {
            var result = await _bookingService.BidBooking(bidId);
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
        [Route("all/consumer")]
        [Authorize]
        public async Task<IActionResult> GetConsumerBooking()
        {
            string consumerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (consumerId != null)
            {
                var result = await _bookingService.GetAllForConsumer(consumerId);

                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("all/Technician")]
        [Authorize]
        public async Task<IActionResult> GetTechnicianBooking()
        {
            string technicianId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (technicianId != null)
            {
                //call the service to get the result

                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
