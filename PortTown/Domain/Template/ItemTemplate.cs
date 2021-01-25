using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Template
{
    public class ItemTemplate
    {
        // ********************** SWORD **********************
        private static readonly ICollection<ResourceBatch> swordCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 15
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Coal,
                Quantity = 30
            }
        };
        private static readonly DateTime timeToBuildSword = new DateTime().AddMinutes(180);
        private static readonly string swordName = "Sword";
        private static readonly int swordValue = 20;

        // ********************** SHIELD **********************
        private static readonly ICollection<ResourceBatch> shieldCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 15
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Coal,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildShield = new DateTime().AddMinutes(90);
        private static readonly string shieldName = "Shield";
        private static readonly int shieldValue = 8;
    }
}
