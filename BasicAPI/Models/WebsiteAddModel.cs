using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Models
{
    public class WebsiteAddModel : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(250, ErrorMessage = "Max length is 250 symbols.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "URL is required.")]
        [MaxLength(250, ErrorMessage = "Max length is 250 symbols.")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Homepage snapshot is required.")]
        public string HomepageSnapshot { get; set; }

        [Required(ErrorMessage = "Login is required.")]
        public UserModel Login { get; set; }

        [Required(ErrorMessage = "Category code is required.")]
        public string CategoryCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                results.Add(new ValidationResult($"Invalid URL."));
            }

            return results;
        }
    }
}