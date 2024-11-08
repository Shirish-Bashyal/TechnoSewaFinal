using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoSewaMaui.Response
{
    public class UserSignInResponse
    {
        public string? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
