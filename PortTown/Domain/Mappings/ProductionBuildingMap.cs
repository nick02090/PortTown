using Domain.Enums;
using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class ProductionBuildingMap : ClassMap<ProductionBuilding>
    {
        public ProductionBuildingMap()
        {
            Table("ProductionBuilding");

            Id(x => x.Id).GeneratedBy.GuidNative();

            // Craftable
            // requiredResources
            // timeToBuild

            // Building
            Map(x => x.Name);
            Map(x => x.Level);
            Map(x => x.Capacity);
            Map(x => x.BuildingType).CustomType<BuildingType>();
            Map(x => x.Town.Id).Column("TownId");
            References(x => x.Town).ForeignKey("TownId").Cascade.None();

            // ProductionBuilding
            // resourceProduced
            // productionRate
            // lastHarvestTime
        }
    }
}
