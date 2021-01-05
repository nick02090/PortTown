using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Town
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }

        public List<Building> Buildings { get; set; }
        public List<Item> Items { get; set; }
    }
}
