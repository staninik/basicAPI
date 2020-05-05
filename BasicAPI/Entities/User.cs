using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicAPI.Entities
{
    public class User
    {
        public User()
        {
            Websites = new HashSet<Website>();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        public virtual ICollection<Website> Websites { get; set; }
    }
}