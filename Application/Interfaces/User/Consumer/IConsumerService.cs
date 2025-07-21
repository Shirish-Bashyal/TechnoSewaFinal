using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.User.Consumer;
using Application.Response;

namespace Application.Interfaces.User.Consumer
{
    public interface IConsumerService
    {
        public Task<ServiceResponse<object>> GetAll();
    }
}
