using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DesktopApp.Models;

namespace DesktopApp.Models
{
    public class Town
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public ICollection<Building> Buildings { get; set; }
        public ICollection<Item> Items { get; set; }
        public User User { get; set; }
        public Upgradeable Upgradeable { get; set; }

        public Town()
        {
            Buildings = new List<Building>();
            Items = new List<Item>();
            Level = 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
