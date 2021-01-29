using Domain.Enums;
using System;
using System.Collections.Generic;

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
        private static readonly int swordPrice = 20;


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
        private static readonly int shieldPrice = 8;


        // ********************** NECKLACE **********************
        private static readonly ICollection<ResourceBatch> necklaceCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 5
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Coal,
                Quantity = 15
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildNecklace = new DateTime().AddMinutes(360);
        private static readonly string necklaceName = "Necklace";
        private static readonly int necklacePrice = 30;


        // ********************** POTTERY **********************
        private static readonly ICollection<ResourceBatch> potteryCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Coal,
                Quantity = 5
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 15
            }
        };
        private static readonly DateTime timeToBuildPottery = new DateTime().AddMinutes(5);
        private static readonly string potteryName = "Pottery";
        private static readonly int potteryPrice = 5;


        public static IEnumerable<Item> Template()
        {
            var items = new List<Item>();

            // Sword
            var swordCraftable = BaseTemplate.GenerateCraftable(swordCost, timeToBuildSword, CraftableType.Item);
            var swordSellable = BaseTemplate.GenerateSellable(swordPrice);
            var swordItem = BaseTemplate.GenerateItem(swordCraftable, swordName, swordSellable);
            items.Add(swordItem);
            // Shield
            var shieldCraftable = BaseTemplate.GenerateCraftable(shieldCost, timeToBuildShield, CraftableType.Item);
            var shieldSellable = BaseTemplate.GenerateSellable(shieldPrice);
            var shieldItem = BaseTemplate.GenerateItem(shieldCraftable, shieldName, shieldSellable);
            items.Add(shieldItem);
            // Necklace 
            var necklaceCraftable = BaseTemplate.GenerateCraftable(necklaceCost, timeToBuildNecklace, CraftableType.Item);
            var necklaceSellable = BaseTemplate.GenerateSellable(necklacePrice);
            var necklaceItem = BaseTemplate.GenerateItem(necklaceCraftable, necklaceName, necklaceSellable);
            items.Add(necklaceItem);
            // Pottery 
            var potteryCraftable = BaseTemplate.GenerateCraftable(potteryCost, timeToBuildPottery, CraftableType.Item);
            var potterySellable = BaseTemplate.GenerateSellable(potteryPrice);
            var potteryItem = BaseTemplate.GenerateItem(potteryCraftable, potteryName, potterySellable);
            items.Add(potteryItem);

            return items;
        }
    }
}
