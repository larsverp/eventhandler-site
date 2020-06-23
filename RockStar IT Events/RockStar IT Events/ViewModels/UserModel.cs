using System.ComponentModel.DataAnnotations;

namespace RockStar_IT_Events.ViewModels
{
    public class UserModel
    {
        [Required(ErrorMessage = "Enter a email address")]
        [StringLength(191, MinimumLength = 4, ErrorMessage = "Username length must be between 4 and 191 characters")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter a Password")]
        [StringLength(191, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 191 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
