using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public abstract class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Info { get; set; }

        public string ImagePath { get; set; }

        public Building(Guid Id, string Name, int Level, string Info, string ImagePath)
        {
            this.Id = Id;
            this.Name = Name;
            this.Level = Level;
            this.Info = Info;
            this.ImagePath = ImagePath;
        }
    }
}