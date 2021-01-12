using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class Craftable
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual IDictionary<ResourceType, int> RequiredResources { get; set; }
        public virtual DateTime TimeToBuild { get; set; }
        public virtual DateTime? TimeUntilCrafted { get; set; }
    }
}
