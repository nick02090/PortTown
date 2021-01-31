using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Buildings;

namespace WebApp.Models.TownViewModels
{
    public class Craftable
    {
        public bool CanCraft { get; set; }
        public bool IsFinishedCrafting { get; set; }
        public Building Building { get; set; }
        public List<Resources.Resource> RequiredResources { get; set; }

        public Craftable(bool CanCraft, Building Building)
        {
            this.Building = Building;
            this.CanCraft = CanCraft;
            this.RequiredResources = new List<Resources.Resource>();
            this.IsFinishedCrafting = false;
        }
    }
}