using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Auth;
using Application.Response;

namespace Application.Interfaces.User.Auth
{
    public interface IAuthServices
    {
        //signin

        Task<ServiceResponse<object>> SignIn(SignInDTO SignInModel);

        //register


        //forget password



        //logout
        Task<ServiceResponse<object>> SignOut();

        //
    }
}
