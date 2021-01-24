using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Template
{
    public class ProductionBuildingTemplate
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
        private static readonly int stoneProductionRate;
        private static readonly ResourceType stoneResourceType = ResourceType.Stone;
        private static readonly string stoneName = "Stonecraft";

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
        private static readonly int woodProductionRate;
        private static readonly ResourceType woodResourceType = ResourceType.Wood;
        private static readonly string woodName = "Sawmill";

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
        private static readonly int goldProductionRate;
        private static readonly ResourceType goldResourceType = ResourceType.Gold;
        private static readonly string goldName = "Goldsmith";

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
        private static readonly int foodProductionRate;
        private static readonly ResourceType foodResourceType = ResourceType.Food;
        private static readonly string foodName = "Food Factory";

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
        private static readonly int ironProductionRate;
        private static readonly ResourceType ironResourceType = ResourceType.Iron;
        private static readonly string ironName = "Steel Factory";

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
        private static readonly int coalProductionRate;
        private static readonly ResourceType coalResourceType = ResourceType.Coal;
        private static readonly string coalName = "Coal Factory";

        public static IEnumerable<ProductionBuilding> Template()
        {
            var productionBuildings = new List<ProductionBuilding>();

            // Production building for Stone
            var stoneCraftable = BaseTemplate.GenerateCraftable(stoneCost, timeToBuildStone, CraftableType.Building);
            var stoneBuilding = BaseTemplate.GenerateBuilding(stoneCraftable, stoneName, stoneCapacity, BuildingType.Production);
            var stoneProductionBuilding = BaseTemplate.GenerateProductionBuilding(stoneBuilding, stoneProductionRate, stoneResourceType);
            productionBuildings.Add(stoneProductionBuilding);
            // Production building for Wood
            var woodCraftable = BaseTemplate.GenerateCraftable(woodCost, timeToBuildWood, CraftableType.Building);
            var woodBuilding = BaseTemplate.GenerateBuilding(woodCraftable, woodName, woodCapacity, BuildingType.Production);
            var woodProductionBuilding = BaseTemplate.GenerateProductionBuilding(woodBuilding, woodProductionRate, woodResourceType);
            productionBuildings.Add(woodProductionBuilding);
            // Production building for Gold
            var goldCraftable = BaseTemplate.GenerateCraftable(goldCost, timeToBuildGold, CraftableType.Building);
            var goldBuilding = BaseTemplate.GenerateBuilding(goldCraftable, goldName, goldCapacity, BuildingType.Production);
            var goldProductionBuilding = BaseTemplate.GenerateProductionBuilding(goldBuilding, goldProductionRate, goldResourceType);
            productionBuildings.Add(goldProductionBuilding);
            // Production building for Food
            var foodCraftable = BaseTemplate.GenerateCraftable(foodCost, timeToBuildFood, CraftableType.Building);
            var foodBuilding = BaseTemplate.GenerateBuilding(foodCraftable, foodName, foodCapacity, BuildingType.Production);
            var foodProductionBuilding = BaseTemplate.GenerateProductionBuilding(foodBuilding, foodProductionRate, foodResourceType);
            productionBuildings.Add(foodProductionBuilding);
            // Production building for Iron
            var ironCraftable = BaseTemplate.GenerateCraftable(ironCost, timeToBuildIron, CraftableType.Building);
            var ironBuilding = BaseTemplate.GenerateBuilding(ironCraftable, ironName, ironCapacity, BuildingType.Production);
            var ironProductionBuilding = BaseTemplate.GenerateProductionBuilding(ironBuilding, ironProductionRate, ironResourceType);
            productionBuildings.Add(ironProductionBuilding);
            // Production building for Coal
            var coalCraftable = BaseTemplate.GenerateCraftable(coalCost, timeToBuildCoal, CraftableType.Building);
            var coalBuilding = BaseTemplate.GenerateBuilding(coalCraftable, coalName, coalCapacity, BuildingType.Production);
            var coalProductionBuilding = BaseTemplate.GenerateProductionBuilding(coalBuilding, coalProductionRate, coalResourceType);
            productionBuildings.Add(coalProductionBuilding);

            return productionBuildings;
        }
    }
}
