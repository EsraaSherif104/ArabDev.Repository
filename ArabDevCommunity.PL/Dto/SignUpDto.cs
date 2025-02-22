using System.ComponentModel.DataAnnotations;

namespace ArabDevCommunity.PL.Dto
{
    public class SignUpDto
    {
        // ال هيتبعت من الفرونت

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
         ErrorMessage = "Password must be at least 8 characters and contain at least 1 uppercase, 1 lowercase , 1 digit, and 1 special character.")]

        public string Password { get; set; }
    }
}
