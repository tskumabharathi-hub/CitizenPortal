using System.ComponentModel.DataAnnotations;

namespace CitizenPortal.DTO.Auth
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;      
        
        [Required]
        public string Password { get; set; } = string.Empty;

        public string OTP { get; set; } = string.Empty;
    }
}
