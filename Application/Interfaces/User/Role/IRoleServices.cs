using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Response;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.User.Role
{
    public interface IRoleServices
    {
        Task<ServiceResponse<object>> CreateRole(string roleName);

        Task<ServiceResponse<object>> ChangeRole(string userId, string roleName);
    }
}
