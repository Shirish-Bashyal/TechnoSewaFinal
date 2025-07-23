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
    public interface ITechnicianService
    {
        Task<ServiceResponse<object>> BecomeTechnician(BecomeTechnicianDTO model, string userId);

        //
        Task<ServiceResponse<List<GetTechnicianDetailsDTO>>> GetByFilter(GetByFilterDTO model);

        Task<ServiceResponse<object>> GetById(int TechnicianId);

        Task<ServiceResponse<object>> GetAll();
    }
}
