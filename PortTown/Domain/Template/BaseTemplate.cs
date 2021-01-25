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

        public static Item GenerateItem(Craftable parentCraftable, string name,
            int value)
        {
            return new Item
            {
                ParentCraftable = parentCraftable,
                Name = name,
                Value = value
            };
        }

        public static Building GenerateBuilding(Craftable parentCraftable, string name, 
            int capacity, BuildingType buildingType)
        {
            return new Building
            {
                ParentCraftable = parentCraftable,
                Name = name,
                BuildingType = buildingType,
                Capacity = capacity,
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
