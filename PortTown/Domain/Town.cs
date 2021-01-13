using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain
{
    public class Town
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Level { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<ProductionBuilding> ProductionBuildings { get; set; }
        public virtual ICollection<Storage> Storages { get; set; }
        public virtual ICollection<Silo> Silos { get; set; }
        public virtual ICollection<Item> Items { get; set; }

        public Town()
        {
            Buildings = new List<Building>();
            ProductionBuildings = new List<ProductionBuilding>();
            Storages = new List<Storage>();
            Silos = new List<Silo>();
            Items = new List<Item>();
            Level = 1;
        }
    }
}
