using System.ComponentModel.DataAnnotations;

namespace RockStar_IT_Events.ViewModels
{
    public class UserModel
    {
        [Required]
        [StringLength(191, MinimumLength = 4, ErrorMessage = "Username length must be between 4 and 191 characters")]
        [DataType(DataType.EmailAddress)]
        public string username { get; set; }

        [Required]
        [StringLength(191, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 191 characters")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
