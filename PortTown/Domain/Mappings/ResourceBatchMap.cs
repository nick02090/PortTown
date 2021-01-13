﻿using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class ResourceBatchMap : ClassMap<ResourceBatch>
    {
        public ResourceBatchMap()
        {
            // Table
            Table("ResourceBatch");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.ResourceType);
            Map(x => x.Size);

            // Relations
            References(x => x.Craftable, "CraftableId").Cascade.None(); // MANY-TO-ONE
        }
    }
}
