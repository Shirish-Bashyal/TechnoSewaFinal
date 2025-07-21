using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Booking;
using Application.DTO.Technician;
using Application.Response;

namespace Application.Interfaces.Bookings
{
    public interface IBookingService
    {
        Task<ServiceResponse<object>> SubCategoryBooking(
            SubCategoryBookingDTO Model,
            string ConsumerId
        );

        Task<ServiceResponse<object>> BidBooking(int BidId);

        Task<ServiceResponse<object>> GetAllForConsumer(string ConsumerId);

        Task<ServiceResponse<object>> GetAllForTechnician(string TechnicianUserId);

        Task<ServiceResponse<object>> GetAll();

        Task<ServiceResponse<object>> MarkBookingCompletion(int BookingId, string UserId);

        //get bookings for a user,
    }
}
