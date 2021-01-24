using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ResourceBatch
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual ResourceType ResourceType { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Craftable Craftable { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
