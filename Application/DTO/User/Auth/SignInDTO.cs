using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User.Auth
{
    public class SignInDTO
    {
        [Required]
        [StringLength(10)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
