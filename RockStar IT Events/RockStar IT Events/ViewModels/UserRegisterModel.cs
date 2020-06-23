using System.ComponentModel.DataAnnotations;

namespace RockStar_IT_Events.ViewModels
{
    public class UserRegisterModel : UserModel
    {
        [Required(ErrorMessage = "Enter a first name")]
        [StringLength(maximumLength:191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string FirstName { get; set; }

        [StringLength(maximumLength: 191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string Insertion { get; set; }

        [Required(ErrorMessage = "Enter a last name")]
        [StringLength(maximumLength: 191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string LastName { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode{ get; set; }

        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }
}