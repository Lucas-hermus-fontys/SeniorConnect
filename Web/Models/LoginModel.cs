using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "E-mail is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailformaat.")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
