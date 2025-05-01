using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Chatbot
{
    public class ImageDto
    {
        public IFormFile Image {  get; set; }
        public string Text { get; set; }
    }
}
