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

        private static readonly DateTime timeToUpgradeStone = new DateTime().AddSeconds(60);
        private static readonly float stoneUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> stoneUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 30
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 100
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 200
            }
        };

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

        private static readonly DateTime timeToUpgradeWood = new DateTime().AddSeconds(60);
        private static readonly float woodUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> woodUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 20
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 40
            }
        };

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

        private static readonly DateTime timeToUpgradeGold = new DateTime().AddSeconds(60);
        private static readonly float goldUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> goldUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 20
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 20
            }
        };

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

        private static readonly DateTime timeToUpgradeFood = new DateTime().AddSeconds(60);
        private static readonly float foodUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> foodUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
            }
        };

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

        private static readonly DateTime timeToUpgradeIron = new DateTime().AddSeconds(60);
        private static readonly float ironUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> ironUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 20
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 40
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 20
            }
        };

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

        private static readonly DateTime timeToUpgradeCoal= new DateTime().AddSeconds(60);
        private static readonly float coalUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> coalUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 40
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 200
            }
        };

        public static IEnumerable<ProductionBuilding> Template()
        {
            var productionBuildings = new List<ProductionBuilding>();

            // Production building for Stone
            var stoneCraftable = BaseTemplate.GenerateCraftable(stoneCost, timeToBuildStone, CraftableType.Building);
            var stoneUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeStone, stoneUpgradeMultiplier, stoneUpgradeCost);
            var stoneBuilding = BaseTemplate.GenerateBuilding(stoneCraftable, stoneName, stoneCapacity, BuildingType.Production, stoneUpgradeable);
            var stoneProductionBuilding = BaseTemplate.GenerateProductionBuilding(stoneBuilding, stoneProductionRate, stoneResourceType);
            productionBuildings.Add(stoneProductionBuilding);
            // Production building for Wood
            var woodCraftable = BaseTemplate.GenerateCraftable(woodCost, timeToBuildWood, CraftableType.Building);
            var woodUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeWood, woodUpgradeMultiplier, woodUpgradeCost);
            var woodBuilding = BaseTemplate.GenerateBuilding(woodCraftable, woodName, woodCapacity, BuildingType.Production, woodUpgradeable);
            var woodProductionBuilding = BaseTemplate.GenerateProductionBuilding(woodBuilding, woodProductionRate, woodResourceType);
            productionBuildings.Add(woodProductionBuilding);
            // Production building for Gold
            var goldCraftable = BaseTemplate.GenerateCraftable(goldCost, timeToBuildGold, CraftableType.Building);
            var goldUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeGold, goldUpgradeMultiplier, goldUpgradeCost);
            var goldBuilding = BaseTemplate.GenerateBuilding(goldCraftable, goldName, goldCapacity, BuildingType.Production, goldUpgradeable);
            var goldProductionBuilding = BaseTemplate.GenerateProductionBuilding(goldBuilding, goldProductionRate, goldResourceType);
            productionBuildings.Add(goldProductionBuilding);
            // Production building for Food
            var foodCraftable = BaseTemplate.GenerateCraftable(foodCost, timeToBuildFood, CraftableType.Building);
            var foodUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeFood, foodUpgradeMultiplier, foodUpgradeCost);
            var foodBuilding = BaseTemplate.GenerateBuilding(foodCraftable, foodName, foodCapacity, BuildingType.Production, foodUpgradeable);
            var foodProductionBuilding = BaseTemplate.GenerateProductionBuilding(foodBuilding, foodProductionRate, foodResourceType);
            productionBuildings.Add(foodProductionBuilding);
            // Production building for Iron
            var ironCraftable = BaseTemplate.GenerateCraftable(ironCost, timeToBuildIron, CraftableType.Building);
            var ironUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeIron, ironUpgradeMultiplier, ironUpgradeCost);
            var ironBuilding = BaseTemplate.GenerateBuilding(ironCraftable, ironName, ironCapacity, BuildingType.Production, ironUpgradeable);
            var ironProductionBuilding = BaseTemplate.GenerateProductionBuilding(ironBuilding, ironProductionRate, ironResourceType);
            productionBuildings.Add(ironProductionBuilding);
            // Production building for Coal
            var coalCraftable = BaseTemplate.GenerateCraftable(coalCost, timeToBuildCoal, CraftableType.Building);
            var coalUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeCoal, coalUpgradeMultiplier, coalUpgradeCost);
            var coalBuilding = BaseTemplate.GenerateBuilding(coalCraftable, coalName, coalCapacity, BuildingType.Production, coalUpgradeable);
            var coalProductionBuilding = BaseTemplate.GenerateProductionBuilding(coalBuilding, coalProductionRate, coalResourceType);
            productionBuildings.Add(coalProductionBuilding);

            return productionBuildings;
        }
    }
}
