using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.TownViewModels
{
    public class TownItemsViewModel
    {
        public List<Items.Item> Items { get; set; }

        public TownItemsViewModel()
        {
            Items = new List<Items.Item>();
        }
    }
}