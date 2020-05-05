using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicAPI.Entities
{
    public class Website : ISoftDeleteEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string URL { get; set; }

        [Required]
        public byte[] HomepageSnapshot { get; set; }

        public Guid UserId { get; set; }

        public virtual User Login { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public string CategoryName
        {
            get { return Category?.Name; }
        }

        [NotMapped]
        public string CategoryCode
        {
            get { return Category?.Code; }
        }
    }
}