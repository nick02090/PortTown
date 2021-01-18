using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class ProductionBuilding
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual ResourceType ResourceProduced { get; set; }
        public virtual int ProductionRate { get; set; }
        public virtual DateTime LastHarvestTime { get; set; }

        // Parent reference
        public virtual Building ParentBuilding { get; set; }

        public ProductionBuilding()
        {
            // ProductionBuilding
            // TODO: Serialize ResourceProduced
            // TODO: Serialize ProductionRate
            LastHarvestTime = DateTime.UtcNow;
        }

        public ProductionBuilding(Building parentBuilding)
        {
            ParentBuilding = parentBuilding;
        }
    }
}
