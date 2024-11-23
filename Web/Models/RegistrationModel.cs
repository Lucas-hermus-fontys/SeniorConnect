using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "E-mail is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig e-mailformaat.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "een Wachtwoord is verplicht.")]
        // [MinLength(8, ErrorMessage = "Het wachtwoord moet minstens 8 tekens lang zijn.")]
        // [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Het wachtwoord moet ten minste één hoofdletter, één kleine letter, één cijfer en één speciaal teken bevatten.")]
        public string Password { get; set; }
    }
}
