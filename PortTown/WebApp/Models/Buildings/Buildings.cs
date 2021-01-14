using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public class Buildings
    {
        public List<Building> BuildingsList { get; set; }

        public Buildings()
        {
            BuildingsList = new List<Building>();
        }
    }
}