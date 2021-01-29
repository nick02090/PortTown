using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Template
{
    public class BaseTemplate
    {
        public static Craftable GenerateCraftable(ICollection<ResourceBatch> cost, 
            DateTime timeToBuild, CraftableType craftableType)
        {
            return new Craftable
            {
                CraftableType = craftableType,
                RequiredResources = cost,
                TimeToBuild = timeToBuild
            };
        }

        public static Upgradeable GenerateUpgradeable(DateTime timeToUpgrade, 
            float upgradeMultiplier, ICollection<ResourceBatch> upgradeCost)
        {
            return new Upgradeable
            {
                TimeToUpgrade = timeToUpgrade,
                UpgradeMultiplier = upgradeMultiplier,
                RequiredResources = upgradeCost
            };
        }

        public static Sellable GenerateSellable(int price)
        {
            return new Sellable()
            {
                Price = price
            };
        }

        public static ResourceBatch GenerateResourceBatch(Sellable sellable, ResourceType resourceType)
        {
            return new ResourceBatch()
            {
                Quantity = 1,
                Sellable = sellable,
                ResourceType = resourceType
            };
        }

        public static Item GenerateItem(Craftable parentCraftable, string name,
            Sellable sellable)
        {
            return new Item
            {
                ParentCraftable = parentCraftable,
                Name = name,
                Sellable = sellable
            };
        }

        public static Building GenerateBuilding(Craftable parentCraftable, string name, 
            int capacity, BuildingType buildingType, Upgradeable buildingUpgradeable)
        {
            return new Building
            {
                ParentCraftable = parentCraftable,
                Name = name,
                BuildingType = buildingType,
                Capacity = capacity,
                Upgradeable = buildingUpgradeable,
                Level = 1
            };
        }

        public static ProductionBuilding GenerateProductionBuilding(Building parentBuilding, 
            float productionRate, ResourceType resourceType)
        {
            return new ProductionBuilding
            {
                ParentBuilding = parentBuilding,
                ProductionRate = productionRate,
                ResourceProduced = resourceType
            };
        }

        public static Storage GenerateStorage(Building parentBuilding, ICollection<ResourceBatch> storedResources)
        {
            return new Storage
            {
                ParentBuilding = parentBuilding,
                StoredResources = storedResources
            };
        }
    }
}
