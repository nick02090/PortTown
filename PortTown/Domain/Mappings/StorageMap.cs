using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class StorageMap : ClassMap<Storage>
    {
        public StorageMap()
        {
            // Table
            Table("Storage");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Relations
            HasMany(x => x.StoredResources).KeyColumn("StorageId").Inverse().Cascade.All(); // ONE-TO-MANY
            References(x => x.ParentBuilding, "ParentBuildingId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
