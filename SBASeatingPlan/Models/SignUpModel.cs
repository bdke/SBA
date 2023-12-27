using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace SBASeatingPlan.Models
{
    public class SignUpModel
    {
        [Required]
        public string EntryCode { get; set; }
        [Required]
        [CustomValidation(typeof(SignUpModel), nameof(ValidateEmail))]
        public string Email { get; set; }
        [Required]
        [CustomValidation(typeof(SignUpModel), nameof(ValidatePassword))]
        public string Password { get; set; }
        [Required]
        [CustomValidation(typeof(SignUpModel), nameof(ValidatePassword))]
        public string PasswordConfirm { get; set; }
        public static ValidationResult ValidateEmail(string email, ValidationContext vc)
        {
            try
            {
                var m = new MailAddress(email);
                return ValidationResult.Success;

            }
            catch (FormatException)
            {
                return new("Invalid Email Adress");
            }
        }

        public static ValidationResult ValidatePassword(string passWord, ValidationContext vc)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return new("a normal and captilized char must be included");
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return new("a number must be included");
            if (validConditions == 2)
            {
                char[] special = ['@', '#', '$', '%', '^', '&', '+', '=', '!'];
                if (passWord.IndexOfAny(special) == -1) return new($"Invalid Password, Only {string.Join(' ', special)} are allowed");
                if (passWord.Length < 9) return new("Length Must be greater than 8 characters");
            }
            return ValidationResult.Success;
        }
    }
}
