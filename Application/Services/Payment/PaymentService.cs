using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Payment;
using Application.Interfaces.Data;
using Application.Interfaces.Payment;
using Application.Response;
using Domain.Entities.Application.Payment;

namespace Application.Services.Payment
{
    public class PaymentService : IPaymentServics
    {
        private readonly IUnitOfWork _uow;

        public PaymentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ServiceResponse<object>> AddPayment(
            TransactionDTO Model,
            string TechnicianUserId
        )
        {
            var technician = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                .GetSingleBySpec(x => x.UserId == TechnicianUserId);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Technician not found."
                };
            }

            var payment = new TransactionDetail()
            {
                AddedDate = DateTime.Now,
                Amount = Model.Amount,
                Pid = Model.Pid,
                Technician = technician,
                TechnicianId = technician.Id
            };
            await _uow.AsyncRepositories<TransactionDetail>().AddAsync(payment);
            var result = await _uow.Save();
            if (result > 0)
            {
                return new ServiceResponse<object> { Success = true, Message = "Payment Addeds" };
            }
            return new ServiceResponse<object> { Success = false, Message = "Operation Failed" };
        }

        public async Task<bool> CheckLimitReached(int TechnicianId)
        {
            var commissionDetails = await _uow.AsyncRepositories<CommissionDetail>()
                .GetSingleBySpec(x => x.TechnicianId == TechnicianId);
            if (commissionDetails == null)
            {
                return false;
            }
            else
            {
                return commissionDetails.IsLimitReached;
            }
        }

        public async Task<ServiceResponse<object>> GetCommissionDetail(string TechnicianUserId)
        {
            var technician = await _uow.AsyncRepositories<Domain.Entities.User.Technician>()
                .GetSingleBySpec(x => x.UserId == TechnicianUserId);
            if (technician == null)
            {
                return new ServiceResponse<object>
                {
                    Success = false,
                    Message = "Technician not found."
                };
            }

            var commissionDetails = await _uow.AsyncRepositories<CommissionDetail>()
                .GetSingleBySpec(x => x.TechnicianId == technician.Id);
            if (commissionDetails == null)
            {
                return new ServiceResponse<object> { Success = true, Message = "No commission" };
            }

            return new ServiceResponse<object> { Success = true, Data = commissionDetails };
        }
    }
}
