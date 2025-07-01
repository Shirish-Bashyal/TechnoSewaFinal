using Application.DTO.Chatbot;
using Application.Interfaces.Chatbot;
using Application.Interfaces.LLM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TechnoSewa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly ITextTokenizer _tokenizer;
        private readonly IQuestionResponseService _responseService;
        private readonly IChatbotService _chatbotService;

        public ChatbotController(
            IChatbotService chatbotService,
            ITextTokenizer tokenizer,
            IQuestionResponseService responseService
        )
        {
            _tokenizer = tokenizer;
            _responseService = responseService;
            _chatbotService = chatbotService;
        }

        [HttpPost]
        [Route("postQuestions")]
        public async Task<IActionResult> PostChat([FromBody] MessageDto question)
        {
            if (ModelState.IsValid)
            {
                //find out the intent of the question

                //then call respective service

                //var result = await _chatbotService.FindIntent(question.Question);
                //return Ok(result);
                var result = await _chatbotService.SendMessage(question);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("postAudio")]
        public async Task<IActionResult> PostAudio([FromForm] AudioDto audio)
        {
            if (ModelState.IsValid)
            {
                var result = await _chatbotService.SendAudio(audio);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("postImage")]
        public async Task<IActionResult> PostImage([FromForm] ImageDto image)
        {
            if (ModelState.IsValid)
            {
                var result = await _chatbotService.SendImage(image);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("tokenizer")]
        public async Task<IActionResult> TextTokenizer([FromBody] MessageDto question)
        {
            if (ModelState.IsValid)
            {
                var result = await _tokenizer.GetMatchedStrings(question);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }



        [HttpPost]
        [Route("addKeywordAndResponse")]
        public async Task<IActionResult> AddKeywordAndResponse(AddDbResponseDto request)
        {
            if (ModelState.IsValid)
            {
                var result = await _responseService.AddKeywordAndResponse(request);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
