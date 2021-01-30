using DesktopApp.Enums;
using DesktopApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesktopApp
{
    public class Craftable
    {
        public Guid Id { get; set; }
        public ICollection<ResourceBatch> RequiredResources { get; set; }
        public DateTime TimeToBuild { get; set; }
        public DateTime? TimeUntilCrafted { get; set; }
        public CraftableType CraftableType { get; set; }
        public bool IsFinishedCrafting { get; set; }

        // References to children
        public Building ChildBuilding { get; set; }
        public Item ChildItem { get; set; }

        public Craftable()
        {
            RequiredResources = new List<ResourceBatch>(); // SERIALIZE
            // SERIALIZE: TimeToBuild
            TimeUntilCrafted = null;
            IsFinishedCrafting = false;
        }
    }
}
