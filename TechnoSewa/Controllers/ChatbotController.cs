using Application.DTO.Chatbot;
using Application.Interfaces.LLM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        public readonly ILLMFormatter _llmFormatter;
        private readonly ITextTokenizer _tokenizer;
        private readonly IQuestionResponseService _responseService;

        public ChatbotController(ILLMFormatter llmFormatter, ITextTokenizer tokenizer, IQuestionResponseService responseService)
        {
            _llmFormatter = llmFormatter;
            _tokenizer = tokenizer;
            _responseService = responseService;
        }

        [HttpPost("/postQuestions")]
        public async Task<IActionResult> PostChat([FromBody] MessageDto question)
        {
            if (ModelState.IsValid)
            {
                var result = await _llmFormatter.FormatMessage(question);
                return Ok(result);
            }
            else { return BadRequest(); }
        }
        [HttpPost("/postAudio")]
        public async Task<IActionResult> PostAudio([FromForm] AudioDto audio)
        {
            if (ModelState.IsValid)
            {
                var result = await _llmFormatter.FormatAudio(audio);
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpPost("/tokenizer")]
        public async Task<IActionResult> TextTokenizer([FromBody] MessageDto question)
        {
            if (ModelState.IsValid)
            {
                var result = await _tokenizer.GetMatchedStrings(question);
                return Ok(result);
            }
            else { return BadRequest(); }
        }

        [HttpPost("/addKeywordAndResponse")]
        public async Task<IActionResult> AddKeywordAndResponse(AddDbResponseDto request)
        {

            if (ModelState.IsValid)
            {
                var result = await _responseService.AddKeywordAndResponse(request);
                return Ok(result);
            }
            else { return BadRequest(); }

        }
    }
}
