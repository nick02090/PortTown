using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Template
{
    public class TownTemplate
    {

        private static readonly ICollection<Building> buildings = new List<Building>()
        {
            new Building()
            {
                Name = "Stone Storage",
                BuildingType = BuildingType.Storage

            },
            new Building()
            {
                Name = "Wood Shed",
                BuildingType = BuildingType.Storage

            },
            new Building()
            {
                Name = "Vault",
                BuildingType = BuildingType.Storage

            }

        };

        private static readonly DateTime timeToUpgradeTown = new DateTime().AddSeconds(60);
        private static readonly float townUpgradeMultiplier = 2.0f;
        private static readonly ICollection<ResourceBatch> townUpgradeCost = new List<ResourceBatch>()
        {
            new ResourceBatch()
            {
                ResourceType = ResourceType.Gold,
                Quantity = 50
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Wood,
                Quantity = 500
            },
            new ResourceBatch()
            {
                ResourceType = ResourceType.Stone,
                Quantity = 500
            }
        };

        public static Town Template()
        {
            var townUpgradeable = BaseTemplate.GenerateUpgradeable(timeToUpgradeTown, townUpgradeMultiplier, townUpgradeCost);
            var town = BaseTemplate.GenerateTown(townUpgradeable, buildings);
            return town;

        }
    }
}
