using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User.Auth
{
    public class OtpVerifyDTO
    {
        [StringLength(10)]
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [StringLength(5)]
        [Required]
        public string Otp { get; set; }
    }
}
