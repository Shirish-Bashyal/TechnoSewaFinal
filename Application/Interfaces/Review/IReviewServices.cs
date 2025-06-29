using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Review;
using Application.DTO.User.Post;
using Application.Response;

namespace Application.Interfaces.Review
{
    public interface IReviewServices
    {
        Task<ServiceResponse<object>> CreateReview(CreateReviewDTO Model);

        Task<ServiceResponse<GetReviewDTO>> GetForTechnician(int TechnicianId);
    }
}
