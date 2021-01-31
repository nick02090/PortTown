using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesktopApp.Models
{
    public class Upgradeable
    {
        public Guid Id { get; set; }
        public ICollection<ResourceBatch> RequiredResources { get; set; }
        public DateTime TimeToUpgrade { get; set; }
        public DateTime? TimeUntilUpgraded { get; set; }
        public bool IsFinishedUpgrading { get; set; }
        public float UpgradeMultiplier { get; set; }

        // References
        public Town Town { get; set; }
        public Building Building { get; set; }

        public Upgradeable()
        {
            RequiredResources = new List<ResourceBatch>(); // SERIALIZE
            // SERIALIZE: TimeToUpgrade
            TimeUntilUpgraded = null;
            IsFinishedUpgrading = false;
        }
    }
}
