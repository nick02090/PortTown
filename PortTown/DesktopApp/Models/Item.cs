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
        public string Name;
        public int Value;
        public ItemQuality itemQuality;

        public Guid townId;

        public Item(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
