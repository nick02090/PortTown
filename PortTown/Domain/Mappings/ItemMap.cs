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
            References(x => x.ParentCraftable, "ParentCraftableId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
