using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class TownMap : ClassMap<Town>
    {
        public TownMap()
        {
            // Table
            Table("Town");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.Name);
            Map(x => x.Level);

            // Relations
            HasMany(x => x.Buildings).KeyColumn("TownId").Inverse().Cascade.All(); // ONE-TO-MANY
            HasMany(x => x.Items).KeyColumn("TownId").Inverse().Cascade.All(); // ONE-TO-MANY
            References(x => x.User, "UserId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
