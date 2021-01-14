using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Resources;

namespace WebApp.Models.Buildings
{
    public class StorageBuilding : Building
    {
        public List<Resource> StoredResources { get; set; }

        public StorageBuilding(string Name, int Level, List<Resource> StoredResources) : base(Name, Level)
        {
            this.StoredResources = StoredResources;
        }
    }
}