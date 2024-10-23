using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.User.Auth
{
    public class RegisterUserDTO
    {
        [StringLength(10)]
        [Phone]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int WardNo { get; set; }

        [Required]
        public string ToleName { get; set; }
    }
}
