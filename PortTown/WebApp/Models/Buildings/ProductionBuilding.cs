using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Resources;

namespace WebApp.Models.Buildings
{
    public class ProductionBuilding : Building
    {
        public Resource Resource { get; set; }
        public Resource MaxResource { get; set; }

        public ProductionBuilding(Guid Id, string Name, int Level, string Info, string ImagePath, bool CanUpgrade, Resource Resource) : 
            base(Id, Name, Level, Info, ImagePath, CanUpgrade)
        {
            this.Resource = Resource;
        }
    }
}