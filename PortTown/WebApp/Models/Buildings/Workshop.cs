using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public class Workshop : Building
    {
        public Items.Item Item { get; set; }
        public Workshop(Guid Id, int Level, string ImagePath, Items.Item Item) : base(Id, "Workshop", Level, Utils.BuildingsInfo.WORKSHOP_INFO, ImagePath)
        {
            this.Item = Item;
        }

        public Workshop(Guid Id, int Level, string ImagePath) : base(Id, "Workshop", Level, Utils.BuildingsInfo.WORKSHOP_INFO, ImagePath)
        {
            this.Item = null;
        }
    }
}