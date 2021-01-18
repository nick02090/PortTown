using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Building
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Level { get; set; }
        public virtual int Capacity { get; set; }
        public virtual BuildingType BuildingType { get; set; }
        public virtual Town Town { get; set; }

        // Reference to parent
        public virtual Craftable ParentCraftable { get; set; }
        // References to children
        public virtual Storage ChildStorage { get; set; }
        public virtual ProductionBuilding ChildProductionBuilding { get; set; }

        public Building()
        {
            // TODO: Serialize Name
            Level = 1;
            // TODO: Serialize Capacity
        }

        public Building(Craftable parentCraftable) : base()
        {
            ParentCraftable = parentCraftable;
        }
    }
}
