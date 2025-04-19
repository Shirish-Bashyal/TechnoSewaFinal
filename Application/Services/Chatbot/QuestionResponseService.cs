using Application.DTO.Chatbot;
using Application.Interfaces.Data;
using Application.Interfaces.LLM;
using Application.Response;
using Domain.Entities.Chatbot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Chatbot
{
    public class QuestionResponseService : IQuestionResponseService
    {
        private readonly IUnitOfWork _uow;

        public QuestionResponseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ServiceResponse<object>> AddKeywordAndResponse(AddDbResponseDto request)
        {
            var checkKeyword = await _uow.AsyncRepositories<QuestionResponse>().DoesExists(x => x.Keyword == request.Keyword);
            if (!checkKeyword)
            {
                var id = Guid.NewGuid().ToString();
                var requestModel = new QuestionResponse()
                {
                    Id = id,
                    Keyword = request.Keyword,
                    Response = request.Response,
                };
               await _uow.AsyncRepositories<QuestionResponse>().AddAsync(requestModel);
               await _uow.Save();

               
                return new ServiceResponse<object>()
                {
                    Success = true,
                    Message = "Keyword and responses added succssfully"
                };
            }
            else
            {
                return new ServiceResponse<object>()
                {
                    Success = false,
                    Message = "Couldn't add Keyword and responses"
                };
            }
        }

        public async Task<string> GetResponseForKeyword(string keyword)
        {
            //var checkKeyword = await _context.Responses.Where(x => x.Keyword == keyword).FirstOrDefaultAsync();
            var checkKeyword = await _uow.AsyncRepositories<QuestionResponse>().GetSingleBySpec(x => x.Keyword == keyword);
            if (checkKeyword == null)
            {
                return "Keyword doesn't exist ";
            }
            else
            {

                var response = checkKeyword.Response;
                return response;
            }
        }
    }
}
