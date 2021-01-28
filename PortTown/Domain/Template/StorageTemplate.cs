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
                Quantity = 5
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 50
            }
        };
        private static readonly DateTime timeToBuildStone = new DateTime().AddSeconds(15);
        private static readonly int stoneCapacity = 600;
        private static readonly string stoneName = "Stone Storage";
        private static readonly ICollection<ResourceBatch> stoneStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 0
            }
        };

        private static readonly DateTime timeToUpgradeStone = new DateTime().AddSeconds(60);
        private static readonly float stoneUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> stoneUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
            }
        };

        // ********************** WOOD **********************
        private static readonly ICollection<ResourceBatch> woodCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 5
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 50
            }
        };
        private static readonly DateTime timeToBuildWood = new DateTime().AddSeconds(15);
        private static readonly int woodCapacity = 800;
        private static readonly string woodName = "Wood Shed";
        private static readonly ICollection<ResourceBatch> woodStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 0
            }
        };

        private static readonly DateTime timeToUpgradeWood = new DateTime().AddSeconds(60);
        private static readonly float woodUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> woodUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
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
        private static readonly DateTime timeToBuildGold = new DateTime().AddSeconds(15);
        private static readonly int goldCapacity = 200;
        private static readonly string goldName = "Vault";
        private static readonly ICollection<ResourceBatch> goldStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 0
            }
        };

        private static readonly DateTime timeToUpgradeGold = new DateTime().AddSeconds(60);
        private static readonly float goldUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> goldUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 20
            }
        };

        // ********************** FOOD **********************
        private static readonly ICollection<ResourceBatch> foodCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 10
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 100
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 50
            }
        };
        private static readonly DateTime timeToBuildFood = new DateTime().AddSeconds(15);
        private static readonly int foodCapacity = 600;
        private static readonly string foodName = "Silo";
        private static readonly ICollection<ResourceBatch> foodStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Food,
                Quantity = 0
            }
        };

        private static readonly DateTime timeToUpgradeFood = new DateTime().AddSeconds(60);
        private static readonly float foodUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> foodUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 20
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 200
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
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
                Quantity = 100
            }
        };
        private static readonly DateTime timeToBuildIron = new DateTime().AddSeconds(15);
        private static readonly int ironCapacity = 300;
        private static readonly string ironName = "Iron Refinery";
        private static readonly ICollection<ResourceBatch> ironStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Iron,
                Quantity = 0
            }
        };

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
                Quantity = 200
            }
        };

        // ********************** COAL ****Z*****************
        private static readonly ICollection<ResourceBatch> coalCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 15
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 200
            }
        };
        private static readonly DateTime timeToBuildCoal = new DateTime().AddSeconds(15);
        private static readonly int coalCapacity = 300;
        private static readonly string coalName = "Coal Bin";
        private static readonly ICollection<ResourceBatch> coalStoredResources = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Coal,
                Quantity = 0
            }
        };

        private static readonly DateTime timeToUpgradeCoal = new DateTime().AddSeconds(60);
        private static readonly float coalUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> coalUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 30
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 400
            }
        };

        public static IEnumerable<Storage> Template()
        {
            var storages = new List<Storage>();

            // Storage building for Stone
            var stoneCraftable = BaseTemplate.GenerateCraftable(stoneCost, timeToBuildStone, CraftableType.Building);
            var stoneUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeStone, stoneUpgradeMultiplier, stoneUpgradeCost);
            var stoneBuilding = BaseTemplate.GenerateBuilding(stoneCraftable, stoneName, stoneCapacity, BuildingType.Storage, stoneUpgradeable);
            var stoneStorage = BaseTemplate.GenerateStorage(stoneBuilding, stoneStoredResources);
            storages.Add(stoneStorage);
            // Storage building for Wood
            var woodCraftable = BaseTemplate.GenerateCraftable(woodCost, timeToBuildWood, CraftableType.Building);
            var woodUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeWood, woodUpgradeMultiplier, woodUpgradeCost);
            var woodBuilding = BaseTemplate.GenerateBuilding(woodCraftable, woodName, woodCapacity, BuildingType.Storage, woodUpgradeable);
            var woodStorage = BaseTemplate.GenerateStorage(woodBuilding, woodStoredResources);
            storages.Add(woodStorage);
            // Storage building for Gold
            var goldCraftable = BaseTemplate.GenerateCraftable(goldCost, timeToBuildGold, CraftableType.Building);
            var goldUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeGold, goldUpgradeMultiplier, goldUpgradeCost);
            var goldBuilding = BaseTemplate.GenerateBuilding(goldCraftable, goldName, goldCapacity, BuildingType.Storage, goldUpgradeable);
            var goldStorage = BaseTemplate.GenerateStorage(goldBuilding, goldStoredResources);
            storages.Add(goldStorage);
            // Storage building for Food
            var foodCraftable = BaseTemplate.GenerateCraftable(foodCost, timeToBuildFood, CraftableType.Building);
            var foodUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeFood, foodUpgradeMultiplier, foodUpgradeCost);
            var foodBuilding = BaseTemplate.GenerateBuilding(foodCraftable, foodName, foodCapacity, BuildingType.Storage, foodUpgradeable);
            var foodStorage = BaseTemplate.GenerateStorage(foodBuilding, foodStoredResources);
            storages.Add(foodStorage);
            // Storage building for Iron
            var ironCraftable = BaseTemplate.GenerateCraftable(ironCost, timeToBuildIron, CraftableType.Building);
            var ironUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeIron, ironUpgradeMultiplier, ironUpgradeCost);
            var ironBuilding = BaseTemplate.GenerateBuilding(ironCraftable, ironName, ironCapacity, BuildingType.Storage, ironUpgradeable);
            var ironStorage = BaseTemplate.GenerateStorage(ironBuilding, ironStoredResources);
            storages.Add(ironStorage);
            // Storage building for Coal
            var coalCraftable = BaseTemplate.GenerateCraftable(coalCost, timeToBuildCoal, CraftableType.Building);
            var coalUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeCoal, coalUpgradeMultiplier, coalUpgradeCost);
            var coalBuilding = BaseTemplate.GenerateBuilding(coalCraftable, coalName, coalCapacity, BuildingType.Storage, coalUpgradeable);
            var coalStorage = BaseTemplate.GenerateStorage(coalBuilding, coalStoredResources);
            storages.Add(coalStorage);

            return storages;
        }
    }
}
