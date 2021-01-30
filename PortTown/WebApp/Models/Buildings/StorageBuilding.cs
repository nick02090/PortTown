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

        public StorageBuilding(Guid Id, string Name, int Level, string Info, string ImagePath, List<Resource> StoredResources) : base(Id, Name, Level, Info, ImagePath)
        {
            this.StoredResources = StoredResources;
        }
    }
}