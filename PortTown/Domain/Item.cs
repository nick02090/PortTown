using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Item
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ItemQuality Quality { get; set; }
        public virtual Town Town { get; set; }
        public virtual Sellable Sellable { get; set; }

        // Reference to parent
        public virtual Craftable ParentCraftable { get; set; }

        public Item()
        {
            // TODO: Serialize Name
            // TODO: Serialize Value
        }

        public Item(Craftable parentCraftable) : base()
        {
            ParentCraftable = parentCraftable;
        }
    }
}
