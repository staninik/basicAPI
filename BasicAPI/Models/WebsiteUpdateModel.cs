using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Models
{
    public class WebsiteUpdateModel : IValidatableObject
    {
        [MaxLength(250, ErrorMessage = "Max length is 250 symbols.")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage = "Max length is 250 symbols.")]
        public string URL { get; set; }

        public string HomepageSnapshot { get; set; }

        public string CategoryCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!string.IsNullOrWhiteSpace(URL) && !Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                results.Add(new ValidationResult($"Invalid URL."));
            }

            return results;
        }
    }
}