using System.ComponentModel.DataAnnotations;

namespace ePizzaHub.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Email-ID")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }    
    }
}
