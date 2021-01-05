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
        public Guid Id { get; set; }
        public Dictionary<ResourceType, int> RequiredResources { get; set; }
        public DateTime TimeToBuild { get; set; }
    }
}
