using Application.DTO.Review;
using Application.Interfaces.Data;
using Application.Interfaces.Review;
using Application.Response;
using Domain.Entities.Application;
using Domain.Entities.Application.Bookings;

namespace Application.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IUnitOfWork _uow;

        public ReviewServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ServiceResponse<object>> CreateReview(CreateReviewDTO Model)
        {
            var booking = await _uow.AsyncRepositories<Booking>().GetByPrimaryKey(Model.BookingId);
            if (booking == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Booking not found"
                };
            }
            var review = new Review
            {
                Booking = booking,
                ByConsumer = Model.ByConsumer,
                Comment = Model.Comment,
                Rating = Model.Rating,
            };
            await _uow.AsyncRepositories<Review>().AddAsync(review);
            var result = await _uow.Save();
            if (result > 0)
            {
                return new ServiceResponse<object>
                {
                    Success = true,
                    Message = "Review Registered"
                };
            }
            else
            {
                return new ServiceResponse<object> { Success = false, Message = "Operation Error" };
            }
        }
    }
}
