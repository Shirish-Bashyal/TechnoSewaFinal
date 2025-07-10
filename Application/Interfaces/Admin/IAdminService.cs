using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Response;

namespace Application.Interfaces.Admin
{
    public interface IAdminService
    {
        public Task<ServiceResponse<object>> GetOverview();

        public Task<ServiceResponse<object>> VerifyTechnician(int TechnicianId);
    }
}
