using System.ComponentModel.DataAnnotations;

namespace SBASeatingPlan.Models
{
    internal class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
