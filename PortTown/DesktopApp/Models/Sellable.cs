using System;
using System.ComponentModel.DataAnnotations;
using DesktopApp.Models;

namespace DesktopApp.Models
{
    public class Sellable
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public Item Item { get; set; }
        public ResourceBatch ResourceBatch { get; set; }
    }
}
