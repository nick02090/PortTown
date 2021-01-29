using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            // Table
            Table("Item");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.Name);
            Map(x => x.Value);
            Map(x => x.Quality);

            // Relations
            References(x => x.Town, "TownId").Cascade.None(); // MANY-TO-ONE
            References(x => x.ParentCraftable, "ParentCraftableId").Cascade.None(); // ONE-TO-ONE
            HasOne(x => x.Sellable).ForeignKey("ItemId").PropertyRef(nameof(Sellable.Item)); // ONE-TO-ONE
        }
    }
}
