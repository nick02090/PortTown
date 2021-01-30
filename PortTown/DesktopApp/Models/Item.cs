using DesktopApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class Item: TableAddable
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemQuality Quality { get; set; }
        public Town Town { get; set; }
        public Sellable Sellable { get; set; }

        // Reference to parent
        public Craftable ParentCraftable { get; set; }

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
