using Application.DTO.Chatbot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.LLM
{
    public interface ITextTokenizer
    {
        Task<TextTokens> Tokenizer(MessageDto msg);
        Task<List<string>> FuzzySearch(TextTokens tokens);
        Task<List<string>> GetMatchedStrings(MessageDto msg);
        Task<string> GetConcatResponseForMatchedStrings(List<string> request);

        Task<string> GetFinalResponse(MessageDto msg);
    }
}
