using System.ComponentModel.DataAnnotations;

namespace RockStar_IT_Events.ViewModels
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Enter a first name")]
        [StringLength(maximumLength:191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter a insertion")]
        [StringLength(maximumLength: 191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string Insertion { get; set; }

        [Required(ErrorMessage = "Enter a last name")]
        [StringLength(maximumLength: 191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter a email address")]
        [StringLength(maximumLength: 191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        public string EmailAddress { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode{ get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [StringLength(maximumLength: 191, MinimumLength = 1, ErrorMessage = "Enter max. 191 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmedPassword { get; set; }
    }
}