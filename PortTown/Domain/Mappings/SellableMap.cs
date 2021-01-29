using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class SellableMap : ClassMap<Sellable>
    {
        public SellableMap()
        {
            // Table
            Table("Sellable");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.Price);

            // Relations
            References(x => x.Item, "ItemId").Cascade.None(); // ONE-TO-ONE
            References(x => x.ResourceBatch, "ResourceBatchId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
