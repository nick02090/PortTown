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
        public virtual List<Building> Buildings { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
