using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Craftable
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual ICollection<ResourceBatch> RequiredResources { get; set; }
        public virtual DateTime TimeToBuild { get; set; }
        public virtual DateTime? TimeUntilCrafted { get; set; }
        public virtual CraftableType CraftableType { get; set; }
        public virtual bool IsFinishedCrafting { get; set; }

        // References to children
        public virtual Building ChildBuilding { get; set; }
        public virtual Item ChildItem { get; set; }

        public Craftable()
        {
            RequiredResources = new List<ResourceBatch>(); // SERIALIZE
            // SERIALIZE: TimeToBuild
            TimeUntilCrafted = null;
            IsFinishedCrafting = false;
        }
    }
}
