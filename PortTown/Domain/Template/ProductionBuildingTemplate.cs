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
                Quantity = 15
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 50
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
            }
        };
        private static readonly DateTime timeToBuildStone = new DateTime().AddSeconds(15);
        private static readonly int stoneCapacity = 200;
        private static readonly float stoneProductionRate = 0.02778f;   // 100 per hour
        private static readonly ResourceType stoneResourceType = ResourceType.Stone;
        private static readonly string stoneName = "Quarry";

        // ********************** WOOD **********************
        private static readonly ICollection<ResourceBatch> woodCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 20
            }
        };
        private static readonly DateTime timeToBuildWood = new DateTime().AddSeconds(15);
        private static readonly int woodCapacity = 200;
        private static readonly float woodProductionRate = 0.04167f;    // 150 per hour
        private static readonly ResourceType woodResourceType = ResourceType.Wood;
        private static readonly string woodName = "Sawmill";

        // ********************** GOLD **********************
        private static readonly ICollection<ResourceBatch> goldCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildGold = new DateTime().AddSeconds(15);
        private static readonly int goldCapacity = 100;
        private static readonly float goldProductionRate = 0.01389f;    // 50 per hour
        private static readonly ResourceType goldResourceType = ResourceType.Gold;
        private static readonly string goldName = "Gold Mine";

        // ********************** FOOD **********************
        private static readonly ICollection<ResourceBatch> foodCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 50
            }
        };
        private static readonly DateTime timeToBuildFood = new DateTime().AddSeconds(15);
        private static readonly int foodCapacity = 200;
        private static readonly float foodProductionRate = 0.04167f;    // 150 per hour
        private static readonly ResourceType foodResourceType = ResourceType.Food;
        private static readonly string foodName = "Farm";

        // ********************** IRON **********************
        private static readonly ICollection<ResourceBatch> ironCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 50
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 20
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 10
            }
        };
        private static readonly DateTime timeToBuildIron = new DateTime().AddSeconds(15);
        private static readonly int ironCapacity = 100;
        private static readonly float ironProductionRate = 0.01389f;    // 50 per hour
        private static readonly ResourceType ironResourceType = ResourceType.Iron;
        private static readonly string ironName = "Iron Mine";

        // ********************** COAL **********************
        private static readonly ICollection<ResourceBatch> coalCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 20
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 50
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 100
            }
        };
        private static readonly DateTime timeToBuildCoal = new DateTime().AddSeconds(15);
        private static readonly int coalCapacity = 100;
        private static readonly float coalProductionRate = 0.01389f;    // 50 per hour
        private static readonly ResourceType coalResourceType = ResourceType.Coal;
        private static readonly string coalName = "Coal Mine";

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
