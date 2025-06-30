using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Response;

namespace Application.Interfaces
{
    public interface ICategoryServices
    {
        Task<ServiceResponse<object>> SubCategoriesList(int categoryId);

        Task<ServiceResponse<object>> AllCategoriesList();
    }
}
