using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Template
{
    public class StorageTemplate
    {
        // ********************** STONE **********************
        private static readonly ICollection<ResourceBatch> stoneCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildStone;
        private static readonly int stoneCapacity;
        private static readonly string stoneName = "Stonehenge";
        private static readonly ICollection<ResourceBatch> stoneStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 100
            }
        };

        // ********************** WOOD **********************
        private static readonly ICollection<ResourceBatch> woodCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildWood;
        private static readonly int woodCapacity;
        private static readonly string woodName = "Enchanted Woods";
        private static readonly ICollection<ResourceBatch> woodStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 200
            }
        };

        // ********************** GOLD **********************
        private static readonly ICollection<ResourceBatch> goldCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildGold;
        private static readonly int goldCapacity;
        private static readonly string goldName = "Goldmine";
        private static readonly ICollection<ResourceBatch> goldStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 50
            }
        };

        // ********************** FOOD **********************
        private static readonly ICollection<ResourceBatch> foodCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildFood;
        private static readonly int foodCapacity;
        private static readonly string foodName = "Silo";
        private static readonly ICollection<ResourceBatch> foodStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Food,
                Quantity = 500
            }
        };

        // ********************** IRON **********************
        private static readonly ICollection<ResourceBatch> ironCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildIron;
        private static readonly int ironCapacity;
        private static readonly string ironName = "Ironmine";
        private static readonly ICollection<ResourceBatch> ironStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 70
            }
        };

        // ********************** COAL **********************
        private static readonly ICollection<ResourceBatch> coalCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildCoal;
        private static readonly int coalCapacity;
        private static readonly string coalName = "Coalmine";
        private static readonly ICollection<ResourceBatch> coalStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Coal,
                Quantity = 70
            }
        };

        public static IEnumerable<Storage> Template()
        {
            var storages = new List<Storage>();

            // Storage building for Stone
            var stoneCraftable = BaseTemplate.GenerateCraftable(stoneCost, timeToBuildStone, CraftableType.Building);
            var stoneBuilding = BaseTemplate.GenerateBuilding(stoneCraftable, stoneName, stoneCapacity, BuildingType.Storage);
            var stoneStorage = BaseTemplate.GenerateStorage(stoneBuilding, stoneStoredResources);
            storages.Add(stoneStorage);
            // Storage building for Wood
            var woodCraftable = BaseTemplate.GenerateCraftable(woodCost, timeToBuildWood, CraftableType.Building);
            var woodBuilding = BaseTemplate.GenerateBuilding(woodCraftable, woodName, woodCapacity, BuildingType.Storage);
            var woodStorage = BaseTemplate.GenerateStorage(woodBuilding, woodStoredResources);
            storages.Add(woodStorage);
            // Storage building for Gold
            var goldCraftable = BaseTemplate.GenerateCraftable(goldCost, timeToBuildGold, CraftableType.Building);
            var goldBuilding = BaseTemplate.GenerateBuilding(goldCraftable, goldName, goldCapacity, BuildingType.Storage);
            var goldStorage = BaseTemplate.GenerateStorage(goldBuilding, goldStoredResources);
            storages.Add(goldStorage);
            // Storage building for Food
            var foodCraftable = BaseTemplate.GenerateCraftable(foodCost, timeToBuildFood, CraftableType.Building);
            var foodBuilding = BaseTemplate.GenerateBuilding(foodCraftable, foodName, foodCapacity, BuildingType.Storage);
            var foodStorage = BaseTemplate.GenerateStorage(foodBuilding, foodStoredResources);
            storages.Add(foodStorage);
            // Storage building for Iron
            var ironCraftable = BaseTemplate.GenerateCraftable(ironCost, timeToBuildIron, CraftableType.Building);
            var ironBuilding = BaseTemplate.GenerateBuilding(ironCraftable, ironName, ironCapacity, BuildingType.Storage);
            var ironStorage = BaseTemplate.GenerateStorage(ironBuilding, ironStoredResources);
            storages.Add(ironStorage);
            // Storage building for Coal
            var coalCraftable = BaseTemplate.GenerateCraftable(coalCost, timeToBuildCoal, CraftableType.Building);
            var coalBuilding = BaseTemplate.GenerateBuilding(coalCraftable, coalName, coalCapacity, BuildingType.Storage);
            var coalStorage = BaseTemplate.GenerateStorage(coalBuilding, coalStoredResources);
            storages.Add(coalStorage);

            return storages;
        }
    }
}
