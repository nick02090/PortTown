using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class UpgradeableMap : ClassMap<Upgradeable>
    {
        public UpgradeableMap()
        {
            // Table
            Table("Upgradeable");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.TimeToUpgrade);
            Map(x => x.TimeUntilUpgraded, "TimeUntilUpgrade");
            Map(x => x.IsFinishedUpgrading);
            Map(x => x.UpgradeMultiplier);

            // References
            HasMany(x => x.RequiredResources).KeyColumn("UpgradeableId").Inverse().Cascade.All(); // ONE-TO-MANY
            References(x => x.Town, "TownId").Cascade.None(); // ONE-TO-ONE
            References(x => x.Building, "BuildingId").Cascade.None(); // ONE-TO-ONE
        }
    }
}
