using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DesktopApp.Models;

namespace DesktopApp
{
    public class Storage
    {
        public Guid Id { get; set; }
        public ICollection<ResourceBatch> StoredResources { get; set; }

        // Parent reference
        public Building ParentBuilding { get; set; }

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
