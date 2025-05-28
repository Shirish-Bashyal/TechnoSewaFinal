using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Constants.Enums;
using Application.DTO.Technician;
using Application.DTO.User.Consumer;
using Application.DTO.User.Post;
using Application.Interfaces.Data;
using Application.Interfaces.Technician;
using Application.Response;
using AutoMapper;
using Domain.Entities.Application.Bookings;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace Application.Services.Technician
{
    public class BidService : IBidService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public BidService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _uow = uow;
        }

        public async Task<ServiceResponse<object>> CreateBid(
            CreateBidDTO Model,
            string TechnicianUserId
        )
        {
            Domain.Entities.User.Technician? technician =
                await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                    .GetSingleBySpec(x => x.UserId == TechnicianUserId);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Message = "Technician not found",
                    Success = false,
                };
            }
            else
            {
                Post? post = await _uow.AsyncRepositories<Post>().GetByPrimaryKey(Model.PostId);

                if (post == null)
                {
                    return new ServiceResponse<object>
                    {
                        Message = "Post not found",
                        Success = false,
                    };
                }
                else
                {
                    var bid = new PostBid
                    {
                        EstimationPrice = Model.EstimationPrice,
                        SolutionDescription = Model.SolutionDescription,
                        Post = post,
                        Technician = technician,
                        Status = (int)PostStatusEnum.Pending,
                        ServiceDate = Model.ServiceDate
                    };

                    await _uow.AsyncRepositories<PostBid>().AddAsync(bid);
                    var result = await _uow.Save();
                    if (result > 0)
                    {
                        return new ServiceResponse<object>
                        {
                            Message = "Bid posted",
                            Success = true,
                        };
                    }
                    else
                    {
                        return new ServiceResponse<object>
                        {
                            Message = "operation error",
                            Success = false,
                        };
                    }
                }
            }
        }

        public Task<ServiceResponse<object>> DeleteBid(int BidId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> GetAllForPost(int PostId)
        {
            var DoespostExists = await _uow.AsyncRepositories<Post>()
                .DoesExists(x => x.Id == PostId);
            if (!DoespostExists)
            {
                return new ServiceResponse<object> { Message = "post not found", Success = false, };
            }
            else
            {
                var includes = new Expression<Func<PostBid, object>>[]
                {
                    x => x.Technician.User,
                    x => x.Post
                };
                var postBid = await _uow.AsyncRepositories<PostBid>()
                    .GetListWithIncludeAndFilter(includes, x => x.Post.Id == PostId);
                if (postBid != null)
                {
                    var result = postBid
                        .Select(bid => new GetBidDTO
                        {
                            BidId = bid.Id,
                            EstimationPrice = bid.EstimationPrice,
                            ServiceDate = bid.ServiceDate,
                            SolutionDescription = bid.SolutionDescription,
                            TechnicianId = bid.Technician.Id,
                            TechnicianName = bid.Technician.User.UserName
                        })
                        .ToList();

                    return new ServiceResponse<object> { Data = result, Success = true, };
                }
                else
                {
                    return new ServiceResponse<object>
                    {
                        Success = false,
                        Message = "No Bid Found"
                    };
                }
            }
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> GetAllForTechnician(string TechnicianUserId)
        {
            Domain.Entities.User.Technician? technician =
                await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                    .GetSingleBySpec(x => x.UserId == TechnicianUserId);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Message = "Technician not found",
                    Success = false,
                };
            }
            var includes = new Expression<Func<PostBid, object>>[] { x => x.Post };
            var bids = await _uow.AsyncRepositories<PostBid>()
                .GetListWithIncludeAndFilter(includes, x => x.Technician.Id == technician.Id);

            //var bids = await _uow.AsyncRepositories<PostBid>()
            //    .GetListBySpec(x => x.Technician.Id == technician.Id);

            if (bids == null)
            {
                return new ServiceResponse<object> { Message = "No bids yet", Success = false, };
            }
            else
            {
                var result = bids.Select(bid => new GetBidsDTO
                    {
                        BidId = bid.Id,
                        EstimationPrice = bid.EstimationPrice,
                        ServiceDate = bid.ServiceDate,
                        SolutionDescription = bid.SolutionDescription,
                        PostId = bid.Post.Id,
                        PostTitle = bid.Post.Title,
                    })
                    .ToList();

                return new ServiceResponse<object> { Data = result, Success = true, };
            }
        }

        public async Task<ServiceResponse<object>> GetBid(int BidId)
        {
            var bid = await _uow.AsyncRepositories<PostBid>().GetByPrimaryKey(BidId);
            if (bid == null)
            {
                return new ServiceResponse<object> { Message = "Bid not found", Success = false, };
            }
            else
            {
                var includes = new Expression<Func<PostBid, object>>[] { x => x.Technician.User };
                var postBid = await _uow.AsyncRepositories<PostBid>()
                    .GetWithIncludeAndFilter(includes, x => x.Id == BidId);
                if (postBid != null)
                {
                    var result = new GetBidDTO
                    {
                        BidId = postBid.Id,
                        EstimationPrice = postBid.EstimationPrice,
                        ServiceDate = postBid.ServiceDate,
                        SolutionDescription = postBid.SolutionDescription,
                        TechnicianId = postBid.Technician.Id,
                        TechnicianName = postBid.Technician.User.UserName
                    };

                    return new ServiceResponse<object> { Data = result, Success = true, };
                }
                else
                {
                    return new ServiceResponse<object>
                    {
                        Success = false,
                        Message = "No Bid Found"
                    };
                }
            }
        }

        public async Task UpdateOtherBidsStatusAsLostAsync(int postId, int winningBidId)
        {
            var otherBids = await _uow.AsyncRepositories<PostBid>()
                .GetListBySpec(x => x.Post.Id == postId && x.Id != winningBidId);

            foreach (var bid in otherBids)
            {
                bid.Status = (int)PostStatusEnum.BidLost;
                await _uow.AsyncRepositories<PostBid>().UpdateAsync(bid);
            }
        }
    }
}
