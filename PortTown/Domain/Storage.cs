using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Storage
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual ICollection<ResourceBatch> StoredResources { get; set; }

        // Parent reference
        public virtual Building ParentBuilding { get; set; }

        public Storage()
        {
            StoredResources = new List<ResourceBatch>(); // SERIALIZE
        }

        public Storage(Building parentBuilding)
        {
            ParentBuilding = parentBuilding;
        }
    }
}
