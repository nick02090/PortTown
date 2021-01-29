using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Town
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public Dictionary<Guid, Buildings.Building> BuiltBuildings { get; set; }
        public Dictionary<Guid, Buildings.Building> ToBuildBuildings { get; set; }

        public Town(string Name, int Level)
        {
            this.Name = Name;
            this.Level = Level;
            this.ToBuildBuildings = new Dictionary<Guid, Buildings.Building>();
            this.BuiltBuildings = new Dictionary<Guid, Buildings.Building>();
        }

        public Town()
        {
            this.Name = "";
            this.Level = -1;
            this.ToBuildBuildings = new Dictionary<Guid, Buildings.Building>();
            this.BuiltBuildings = new Dictionary<Guid, Buildings.Building>();
        }
    }
}