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
        public virtual int Size { get; set; }
        public virtual ProductionBuilding Craftable { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
