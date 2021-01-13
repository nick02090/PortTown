using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class ResourceBatchMap : ClassMap<ResourceBatch>
    {
        public ResourceBatchMap()
        {
            Table("ResourceBatch");

            Id(x => x.Id).GeneratedBy.GuidNative();

            Map(x => x.ResourceType);
            Map(x => x.Size);
            References(x => x.Craftable, "CraftableId").Cascade.None();
        }
    }
}
