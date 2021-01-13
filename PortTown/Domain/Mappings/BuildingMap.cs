using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class BuildingMap : ClassMap<Building>
    {
        BuildingMap()
        {
            // Table
            Table("Building");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.Name);
            Map(x => x.Level);
            Map(x => x.Capacity);
            Map(x => x.BuildingType);

            // Relations
            References(x => x.Town, "TownId").Cascade.None(); // MANY-TO-ONE
            HasOne(x => x.ChildProductionBuilding).ForeignKey("ParentBuildingId").PropertyRef(nameof(ProductionBuilding.ParentBuilding)); // ONE-TO-ONE
            References(x => x.ParentCraftable, "ParentCraftableId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
