using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models.Buildings;

namespace WebApp.Models.TownViewModels
{
    public class TownBuildingsViewModel
    {
        public List<Building> BuildingsList { get; set; }

        public TownBuildingsViewModel()
        {
            BuildingsList = new List<Building>();
        }
    }
}