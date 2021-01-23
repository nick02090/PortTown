using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public virtual ICollection<Item> Items { get; set; }
        public virtual User User { get; set; }

        public Town()
        {
            Buildings = new List<Building>();
            Items = new List<Item>();
            Level = 1;
        }
    }
}
