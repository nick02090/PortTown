using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class TownMap : ClassMap<Town>
    {
        public TownMap()
        {
            Table("Town");

            Id(x => x.Id).GeneratedBy.GuidNative();

            Map(x => x.Name);
            Map(x => x.Level);
            HasMany(x => x.ProductionBuildings).KeyColumn("TownId").Inverse().Cascade.All();
        }
    }
}
