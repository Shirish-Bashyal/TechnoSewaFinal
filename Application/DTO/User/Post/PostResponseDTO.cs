using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.User.Post
{
    public class PostResponseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<string>? ImageUrl { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }

        public DateTime? CreationDate { get; set; }

        public string UserName { get; set; }
    }
}
