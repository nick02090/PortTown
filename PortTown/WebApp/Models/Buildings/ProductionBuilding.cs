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

        public ProductionBuilding(string Name, int Level, Resource Resource) : base(Name, Level)
        {
            this.Resource = Resource;
        }
    }
}