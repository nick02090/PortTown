using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public class CraftingBuilding
    {
        public Building Building { get; set; }
        public DateTime TimeUntilCrafted { get; set; }
        public bool IsFinishedCrafting { get; set; }

        public CraftingBuilding(Building building, DateTime timeUntilCrafted, bool isFinishedCrafting)
        {
            this.Building = building;
            this.TimeUntilCrafted = timeUntilCrafted;
            this.IsFinishedCrafting = isFinishedCrafting;
        }
    }
}