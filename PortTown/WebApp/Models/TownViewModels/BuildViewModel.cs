using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.TownViewModels
{
    public class BuildViewModel
    {
        public List<Craftable> Craftables { get; set; }

        public BuildViewModel()
        {
            this.Craftables = new List<Craftable>();
        }
    }
}