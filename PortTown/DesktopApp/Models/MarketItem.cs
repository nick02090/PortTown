using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class MarketItem: TableAddable
    {
        public Guid Id { get; set; }
        public string Name;
        public int Capacity;
        public int Level;

        public Guid townId;

        //public Craftable craftable;
        public MarketItem(string name)
        {
            // TODO: Remove this when connected to API
            Id = Guid.NewGuid();

            Name = name;
        }
    }
}
