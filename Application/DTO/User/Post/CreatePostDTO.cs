using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.User.Post
{
    public class CreatePostDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }

        public double Lattitude { get; set; }

        public double Longitude { get; set; }
    }
}
