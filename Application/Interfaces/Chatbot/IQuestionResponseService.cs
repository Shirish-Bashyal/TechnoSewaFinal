using Application.DTO.Chatbot;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Chatbot
{
    public interface IQuestionResponseService
    {
        Task<ServiceResponse<object>> AddKeywordAndResponse(AddDbResponseDto request);
        Task<string> GetResponseForKeyword(string keyword);
    }
}
