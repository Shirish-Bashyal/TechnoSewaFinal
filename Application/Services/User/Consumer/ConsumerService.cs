using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Consumer;
using Application.Interfaces.Data;
using Application.Interfaces.User.Consumer;
using Application.Response;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.User.Consumer
{
    public class ConsumerService : IConsumerService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConsumerService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<object>> GetAll()
        {
            var consumers = await _userManager.GetUsersInRoleAsync("Consumer");

            var includes = new Expression<Func<ApplicationUser, object>>[]
            {
                x => x.Address,
                x => x.Address.City,
            };

            var consumersWithIncludes = await _uow.AsyncRepositories<ApplicationUser>()
                .GetListWithIncludeAndFilter(includes, x => consumers.Contains(x));

            var result = consumersWithIncludes
                .Select(x => new GetAllConsumersDTO
                {
                    Id = x.Id,
                    Name = x.UserName,
                    Address = $"{x.Address.City.Name}-{x.Address.WardNo},{x.Address.ToleName}",
                    Phone = x.PhoneNumber
                })
                .ToList();

            return new ServiceResponse<object> { Success = true, Data = result, };
        }
    }
}
