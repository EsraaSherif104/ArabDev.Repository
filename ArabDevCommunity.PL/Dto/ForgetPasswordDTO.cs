using System.ComponentModel.DataAnnotations;

namespace ArabDevCommunity.PL.Dto
{
    public class ForgetPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
