using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Response;

namespace Application.Interfaces.User.Consumer
{
    public interface IProfileService
    {
        Task<ServiceResponse<object>> ConsumerProfile(string userId);

        //Task<ServiceResponse<object>> EditProfile();
    }
}
