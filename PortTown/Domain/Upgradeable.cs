using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Upgradeable
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual ICollection<ResourceBatch> RequiredResources { get; set; }
        public virtual DateTime TimeToUpgrade { get; set; }
        public virtual DateTime? TimeUntilUpgraded { get; set; }
        public virtual bool IsFinishedUpgrading { get; set; }
        public virtual float UpgradeMultiplier { get; set; }

        // References
        public virtual Town Town { get; set; }
        public virtual Building Building { get; set; }

        public Upgradeable()
        {
            RequiredResources = new List<ResourceBatch>(); // SERIALIZE
            // SERIALIZE: TimeToUpgrade
            TimeUntilUpgraded = null;
            IsFinishedUpgrading = false;
        }
    }
}
