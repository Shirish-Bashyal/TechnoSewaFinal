using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Data;
using Application.Response;
using Domain.Entities.Application;
using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _uow;

        public CategoryServices(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ServiceResponse<object>> SubCategoriesList(int categoryId)
        {
            //var doesExists = await _uow.AsyncRepositories<Category>()
            //    .DoesExists(x => x.Id == categoryId);



            var result = await _uow.AsyncRepositories<SubCategory>()
                .GetListBySpec(x => x.Category.Id == categoryId);
            if (result == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Category doesnot have any subcategory"
                };
            }
            if (result.Any())
            {
                return new ServiceResponse<object> { Success = true, Data = result };
            }
            else
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Category doesnot have any subcategory"
                };
            }
        }
    }
}
