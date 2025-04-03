using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Post;
using Application.Response;

namespace Application.Interfaces.User.Consumer
{
    public interface IPostService
    {
        Task<ServiceResponse<object>> CreatePost(CreatePostDTO model, string userId);

        Task<ServiceResponse<object>> GetPost(int PostId);
        Task<ServiceResponse<object>> GetAllPosts(string UserId);

        Task<ServiceResponse<object>> DeletePost(int PostId);

        Task<ServiceResponse<object>> UpdatePost(int PostId);
    }
}
