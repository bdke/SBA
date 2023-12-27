using System.ComponentModel.DataAnnotations;

namespace SBASeatingPlan.Models
{
    public class SubmittionModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [CustomValidation(typeof(SubmittionModel), nameof(ValidateGradTime))]
        public int GradTime { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Employment { get; set; }
        [Required]
        public int SeatsRequired { get; set; }
        public string EntryCode { get; set; }

        public static ValidationResult ValidateGradTime(int gradTime, ValidationContext vc)
        {
            return gradTime < 2024 ? ValidationResult.Success : new ValidationResult($"Invalid Time {gradTime}", [vc.MemberName]);
        }
    }
}
