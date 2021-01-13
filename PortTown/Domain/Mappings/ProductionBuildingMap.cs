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
            HasMany(x => x.RequiredResources).KeyColumn("CraftableId").Inverse().Cascade.All();
            Map(x => x.TimeToBuild);
            Map(x => x.TimeUntilCrafted);

            // Building
            Map(x => x.Name);
            Map(x => x.Level);
            Map(x => x.Capacity);
            Map(x => x.BuildingType);
            References(x => x.Town, "TownId").Cascade.None();

            // ProductionBuilding
            Map(x => x.ResourceProduced);
            Map(x => x.ProductionRate);
            Map(x => x.LastHarvestTime);
        }
    }
}
