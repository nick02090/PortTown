using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class ProductionBuildingMap : ClassMap<ProductionBuilding>
    {
        public ProductionBuildingMap()
        {
            // Table
            Table("ProductionBuilding");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.ResourceProduced);
            Map(x => x.ProductionRate);
            Map(x => x.LastHarvestTime);

            // Relations
            References(x => x.ParentBuilding, "ParentBuildingId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
