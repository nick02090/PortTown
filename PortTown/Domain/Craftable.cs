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
        public virtual ICollection<ResourceBatch> RequiredResources { get; set; }
        public virtual DateTime TimeToBuild { get; set; }
        public virtual DateTime? TimeUntilCrafted { get; set; }

        public Craftable()
        {
            RequiredResources = new List<ResourceBatch>(); // SERIALIZE
            // SERIALIZE: TimeToBuild
            TimeUntilCrafted = null;
        }
    }
}
