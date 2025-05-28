using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Technician;
using Application.DTO.User.Post;
using Application.Response;

namespace Application.Interfaces.Technician
{
    public interface IBidService
    {
        Task<ServiceResponse<object>> CreateBid(CreateBidDTO Model, string TechnicianUserId);

        Task<ServiceResponse<object>> GetBid(int BidId);

        Task<ServiceResponse<object>> GetAllForPost(int Postd); //gets all bidding for a post

        Task<ServiceResponse<object>> GetAllForTechnician(string TechnicianUserId);

        Task<ServiceResponse<object>> DeleteBid(int BidId);
        Task UpdateOtherBidsStatusAsLostAsync(int postId, int winningBidId);
    }
}
