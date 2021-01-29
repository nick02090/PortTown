using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public abstract class Building
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Info { get; set; }

        public string ImagePath { get; set; }

        public Building(string Name, int Level, string Info, string ImagePath)
        {
            this.Name = Name;
            this.Level = Level;
            this.Info = Info;
            this.ImagePath = ImagePath;
        }
    }
}