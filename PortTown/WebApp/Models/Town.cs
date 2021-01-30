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

        public Town(Guid Id, string Name, int Level, List<Buildings.Building> Buildings)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Buildings =Buildings;
        }

        public Town(Guid Id, string Name, int Level)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Buildings = new List<Buildings.Building>();
        }

        public Town()
        {
            this.Id = Guid.NewGuid();
            this.Name = "";
            this.Level = -1;
            this.Buildings = new List<Buildings.Building>();
        }
    }
}