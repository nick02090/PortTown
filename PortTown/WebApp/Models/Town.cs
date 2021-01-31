using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Town
    {
        public Guid Id;
        public string Name { get; set; }
        public int Level { get; set; }
        public List<Buildings.Building> Buildings { get; set; }
        public List<Buildings.CraftingBuilding> CraftingBuildings { get;set; }
        public List<Buildings.CraftingBuilding> UpgradeingBuildings { get; set; }
        public bool CanUpgrade { get; set; }
        public bool IsUpgrading { get; set; }
        public bool IsFinishUpgrade { get; set; }
        public DateTime TimeToUpgrade { get; set; }

        public Town(Guid Id, string Name, int Level, List<Buildings.Building> Buildings)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Buildings =Buildings;
            this.CanUpgrade = false;
            this.IsUpgrading = false;
            this.IsFinishUpgrade = false;
        }

        public Town(Guid Id, string Name, int Level)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Buildings = new List<Buildings.Building>();
            this.CraftingBuildings = new List<Buildings.CraftingBuilding>();
            this.UpgradeingBuildings = new List<Buildings.CraftingBuilding>();
            this.CanUpgrade = false;
            this.IsUpgrading = false;
            this.IsFinishUpgrade = false;

        }

        public Town()
        {
            this.Id = Guid.NewGuid();
            this.Name = "";
            this.Level = -1;
            this.Buildings = new List<Buildings.Building>();
            this.CraftingBuildings = new List<Buildings.CraftingBuilding>();
            this.UpgradeingBuildings = new List<Buildings.CraftingBuilding>();
            this.CanUpgrade = false;
            this.IsUpgrading = false;
            this.IsFinishUpgrade = false;

        }
    }
}