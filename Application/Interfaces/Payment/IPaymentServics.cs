using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Payment;
using Application.Response;

namespace Application.Interfaces.Payment
{
    public interface IPaymentServics
    {
        public Task<ServiceResponse<object>> AddPayment(
            TransactionDTO Model,
            string TechnicianUserId
        );

        public Task<ServiceResponse<object>> GetCommissionDetail(string TechnicianUserId);

        public Task<bool> CheckLimitReached(int TechnicianId);
    }
}
