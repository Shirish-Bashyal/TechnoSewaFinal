using Application.Interfaces.Admin;
using Application.Interfaces.Bookings;
using Application.Interfaces.Technician;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IBookingService _bookingService;
        private readonly ITechnicianService _technicianService;

        public AdminController(
            IAdminService adminService,
            IBookingService bookingService,
            ITechnicianService technicianService
        )
        {
            _adminService = adminService;
            _bookingService = bookingService;
            _technicianService = technicianService;
        }

        [HttpGet]
        [Route("dashboard")]
        public async Task<IActionResult> GetDashboardDetails()
        {
            var result = await _adminService.GetOverview();
            return Ok(result);
        }

        [HttpGet]
        [Route("bookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("verify/technician")]
        public async Task<IActionResult> VerifyTechnician(int TechnicianId)
        {
            var result = await _adminService.VerifyTechnician(TechnicianId);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
    }
}
