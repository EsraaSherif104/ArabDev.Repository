using System.ComponentModel.DataAnnotations;

namespace ArabDevCommunity.PL.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
