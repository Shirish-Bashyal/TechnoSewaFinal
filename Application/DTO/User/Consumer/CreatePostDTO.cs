using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Application;
using Domain.Entities.User;
using Domain.Entities.User.PostDetails;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.User.Consumer
{
    public class CreatePostDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public List<IFormFile>? ImageFiles { get; set; }

        public Double Lattitude { get; set; }

        public Double Longitude { get; set; }
    }
}
