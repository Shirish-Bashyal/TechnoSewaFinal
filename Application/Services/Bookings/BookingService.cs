using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Constants.Enums;
using Application.DTO.Booking;
using Application.DTO.Booking.Consumer;
using Application.Interfaces.Bookings;
using Application.Interfaces.Data;
using Application.Interfaces.Technician;
using Application.Interfaces.User.Role;
using Application.Response;
using Domain.Entities.Application;
using Domain.Entities.Application.Bookings;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBidService _bidService;

        private readonly UserManager<ApplicationUser> _userManager;

        public BookingService(
            IUnitOfWork uow,
            UserManager<ApplicationUser> userManager,
            IBidService bidService
        )
        {
            _uow = uow;
            _userManager = userManager;
            _bidService = bidService;
        }

        public async Task<ServiceResponse<object>> BidBooking(int BidId)
        {
            var includes = new Expression<Func<PostBid, object>>[] { s => s.Post };
            var bid = await _uow.AsyncRepositories<PostBid>()
                .GetWithIncludeAndFilter(includes, x => x.Id == BidId);

            if (bid == null)
            {
                return new ServiceResponse<object> { Message = "Bid not found", Success = false, };
            }

            //change the status of current bid
            bid.Status = (int)PostStatusEnum.Booked;
            var booking = new Booking { PostBid = bid, Status = (int)PostStatusEnum.Booked };

            await _uow.AsyncRepositories<Booking>().AddAsync(booking);

            //change the status of other bids

            await _bidService.UpdateOtherBidsStatusAsLostAsync(bid.Post.Id, bid.Id);
            // Update the post status
            bid.Post.Status = (int)PostStatusEnum.Booked;
            await _uow.AsyncRepositories<Post>().UpdateAsync(bid.Post);

            var result = await _uow.Save();
            if (result > 0)
            {
                return new ServiceResponse<object>
                {
                    Message = "Technician is Booked",
                    Success = true,
                };
            }
            return new ServiceResponse<object> { Message = "Booking Failure", Success = false, };
        }

        public async Task<ServiceResponse<object>> GetAllForConsumer(string ConsumerId)
        {
            var includes = new Expression<Func<Booking, object>>[]
            {
                x => x.SubCategoryBooking,
                x => x.PostBid,
                x => x.SubCategoryBooking.SubCategory,
                x => x.SubCategoryBooking.TimeFrame,
                x => x.SubCategoryBooking.Technician,
                x => x.SubCategoryBooking.Technician.User,
                x => x.PostBid.Post,
                x => x.PostBid.Technician,
                x => x.PostBid.Technician.User
            };
            var bookings = await _uow.AsyncRepositories<Booking>()
                .GetListWithIncludeAndFilter(
                    includes,
                    x =>
                        (
                            x.SubCategoryBooking != null
                            && x.SubCategoryBooking.ConsumerId == ConsumerId
                        )
                        || (
                            x.PostBid != null
                            && x.PostBid.Post != null
                            && x.PostBid.Post.User != null
                            && x.PostBid.Post.User.Id == ConsumerId
                        )
                );

            var activeBookings = bookings
                .Where(x => x.Status == (int)PostStatusEnum.Booked)
                .Select(a => new ActiveBookingsDTO
                {
                    BookingId = a.Id,
                    TechnicianName =
                        a.SubCategoryBooking == null
                            ? a.PostBid.Technician.User.UserName
                            : a.SubCategoryBooking.Technician.User.UserName,
                    Price =
                        a.SubCategoryBooking == null
                            ? a.PostBid.EstimationPrice
                            : a.SubCategoryBooking.SubCategory.Price,
                    ServiceDate =
                        a.SubCategoryBooking == null
                            ? a.PostBid.ServiceDate
                            : a.SubCategoryBooking.ServiceDate,
                    TimeFrame =
                        a.SubCategoryBooking != null ? a.SubCategoryBooking.TimeFrame : null,
                    Lattitude =
                        a.SubCategoryBooking != null
                            ? a.SubCategoryBooking.Lattitude
                            : a.PostBid.Post.Lattitude,
                    Longitude =
                        a.SubCategoryBooking != null
                            ? a.SubCategoryBooking.Longitude
                            : a.PostBid.Post.Longitude,
                    Title =
                        a.SubCategoryBooking != null
                            ? a.SubCategoryBooking.SubCategory.Title
                            : a.PostBid.Post.Title
                })
                .ToList();

            var completedBookings = bookings
                .Where(x => x.Status == (int)PostStatusEnum.Completed)
                .Select(a => new CompletedBookingsDTO
                {
                    BookingId = a.Id,
                    TechnicianName =
                        a.SubCategoryBooking == null
                            ? a.PostBid.Technician.User.UserName
                            : a.SubCategoryBooking.Technician.User.UserName,
                    Price =
                        a.SubCategoryBooking == null
                            ? a.PostBid.EstimationPrice
                            : a.SubCategoryBooking.SubCategory.Price,
                    ServiceDate =
                        a.SubCategoryBooking == null
                            ? a.PostBid.ServiceDate
                            : a.SubCategoryBooking.ServiceDate,
                    TimeFrame =
                        a.SubCategoryBooking != null ? a.SubCategoryBooking.TimeFrame : null,
                    Lattitude =
                        a.SubCategoryBooking != null
                            ? a.SubCategoryBooking.Lattitude
                            : a.PostBid.Post.Lattitude,
                    Longitude =
                        a.SubCategoryBooking != null
                            ? a.SubCategoryBooking.Longitude
                            : a.PostBid.Post.Longitude,
                    Title =
                        a.SubCategoryBooking != null
                            ? a.SubCategoryBooking.SubCategory.Title
                            : a.PostBid.Post.Title
                })
                .ToList();

            var postIncludes = new Expression<Func<Post, object>>[] { x => x.Bids };

            var pendingPost = await _uow.AsyncRepositories<Post>()
                .GetListWithIncludeAndFilter(
                    postIncludes,
                    x => x.Status == (int)PostStatusEnum.Pending
                );

            var pendingBookings = pendingPost
                .Select(x => new PendingBookingsDTO
                {
                    PostDetails = new DTO.User.Post.PostResponseDTO
                    {
                        Category = x.Category.Name,
                        Description = x.Description,
                        Lattitude = x.Lattitude,
                        Longitude = x.Longitude,

                        Title = x.Title,
                    },
                    PostBids = x.Bids.Select(a => new DTO.User.Consumer.GetBidDTO
                    {
                        BidId = a.Id,
                        ServiceDate = a.ServiceDate,
                        SolutionDescription = a.SolutionDescription,
                        EstimationPrice = a.EstimationPrice,
                        TechnicianId = a.Technician.Id,
                        TechnicianName = a.Technician.User.UserName
                    })
                        .ToList(),
                })
                .ToList();

            return new ServiceResponse<object>
            {
                Data = new GetConsumerBookingsDTO
                {
                    ActiveBookings = activeBookings,
                    CompletedBookings = completedBookings,
                    PendingBookings = pendingBookings
                },
                Message = "",
                Success = true,
            };
            //throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> GetAllForTechnician(int TechnicianId)
        {
            var includes = new Expression<Func<Booking, object>>[]
            {
                x => x.SubCategoryBooking,
                x => x.PostBid,
            };
            var result = await _uow.AsyncRepositories<Booking>()
                .GetListWithIncludeAndFilter(
                    includes,
                    x =>
                        (x.SubCategoryBooking.TechnicianId == TechnicianId)
                        || (x.PostBid.Technician.Id == TechnicianId)
                );
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> SubCategoryBooking(
            SubCategoryBookingDTO Model,
            string ConsumerId
        )
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(ConsumerId);
            if (user != null)
            {
                var category = await _uow.AsyncRepositories<Category>()
                    .GetByPrimaryKey(Model.CategoryId);

                if (category == null)
                {
                    return new ServiceResponse<object>
                    {
                        Message = "Category not found",
                        Success = false,
                    };
                }

                var subCategory = await _uow.AsyncRepositories<SubCategory>()
                    .GetByPrimaryKey(Model.SubCategoryId);

                if (subCategory == null)
                {
                    return new ServiceResponse<object>
                    {
                        Message = "SubCategory not found",
                        Success = false,
                    };
                }

                var timeFrame = await _uow.AsyncRepositories<TimeFrame>()
                    .GetByPrimaryKey(Model.TimeFrame);

                if (timeFrame == null)
                {
                    return new ServiceResponse<object>
                    {
                        Message = "TimeFrame not found",
                        Success = false,
                    };
                }

                var tech = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                    .GetByPrimaryKey(Model.TechnicianID);

                if (tech == null)
                {
                    return new ServiceResponse<object>
                    {
                        Message = "Technician not found",
                        Success = false,
                    };
                }

                var subCategoryBooking = new SubCategoryBooking
                {
                    ServiceDate = Model.ServiceDate,
                    Category = category,
                    SubCategory = subCategory,
                    Consumer = user,
                    Lattitude = Model.Lattitude,
                    Longitude = Model.Longitude,
                    Technician = tech,
                    TimeFrame = timeFrame,
                };

                await _uow.AsyncRepositories<SubCategoryBooking>().AddAsync(subCategoryBooking);

                var booking = new Booking
                {
                    Status = (int)PostStatusEnum.Booked,
                    SubCategoryBooking = subCategoryBooking,
                };
                await _uow.AsyncRepositories<Booking>().AddAsync(booking);

                var result = await _uow.Save();
                if (result > 0)
                {
                    return new ServiceResponse<object>
                    {
                        Message = "Technician is Booked",
                        Success = true,
                    };
                }
                return new ServiceResponse<object> { Message = "Booking Failed", Success = false, };
            }
            else
            {
                return new ServiceResponse<object> { Message = "User not found", Success = false, };
            }
        }
    }
}
