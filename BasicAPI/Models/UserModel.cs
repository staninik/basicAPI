using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace BasicAPI.Models
{
    public class UserModel : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length is 100 symbols.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 symbols.")]
        [MaxLength(30, ErrorMessage = "Maximum length is 30 symbols.")]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!IsEmailValid(Email))
            {
                results.Add(new ValidationResult("Invalid Email."));
            }

            return results;
        }

        private bool IsEmailValid(string email)
        {
            try
            {
                MailAddress ma = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}