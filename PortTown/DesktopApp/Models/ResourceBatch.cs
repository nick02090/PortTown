using System;
using System.ComponentModel.DataAnnotations;
using DesktopApp.Enums;
using DesktopApp.Models;

namespace DesktopApp.Models
{
    public class ResourceBatch
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
        public int Quantity { get; set; }
        public Craftable Craftable { get; set; }
        public Storage Storage { get; set; }
        public Upgradeable Upgradeable { get; set; }
        public Sellable Sellable { get; set; }
    }
}
