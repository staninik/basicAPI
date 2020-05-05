using System;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Code { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}