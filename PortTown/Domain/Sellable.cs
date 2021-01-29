using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Sellable
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual int Price { get; set; }
        public virtual Item Item { get; set; }
        public virtual ResourceBatch ResourceBatch { get; set; }
    }
}
