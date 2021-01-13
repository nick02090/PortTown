using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class CraftableMap : ClassMap<Craftable>
    {
        CraftableMap()
        {
            // Table
            Table("Craftable");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.TimeToBuild);
            Map(x => x.TimeUntilCrafted);
            Map(x => x.CraftableType);

            // Relations
            HasMany(x => x.RequiredResources).KeyColumn("CraftableId").Inverse().Cascade.All(); // ONE-TO-MANY
            HasOne(x => x.ChildBuilding).ForeignKey("ParentCraftableId").PropertyRef(nameof(Building.ParentCraftable)); // ONE-TO-ONE
        }
    }
}
