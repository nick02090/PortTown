using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Items
{
    public class Item
    {
        public List<Resources.Resource> RequiredResources { get; set; }
        public ItemType Type { get; set; }
        public int Amount { get; set; }

        public List<Resources.Resource> ToSell { get; set; }
        public List<Resources.Resource> ToBuy { get; set; }

        public Item(ItemType Type, int Amount)
        {
            this.Type = Type;
            this.Amount = Amount;
            this.RequiredResources = new List<Resources.Resource>();
            this.ToBuy = new List<Resources.Resource>();
            this.ToSell = new List<Resources.Resource>();
        }

        public Item(ItemType Type, int Amount, List<Resources.Resource> resources)
        {
            this.Type = Type;
            this.Amount = Amount;
            this.RequiredResources = resources;
        }
    }
}