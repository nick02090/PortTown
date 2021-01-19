using FluentNHibernate.Mapping;

namespace Domain.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            // Table
            Table("`User`");

            // Id
            Id(x => x.Id).GeneratedBy.GuidNative();

            // Properties
            Map(x => x.Username);
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.Token);

            // Relations
            HasOne(x => x.Town).ForeignKey("UserId").PropertyRef(nameof(Town.User)); // ONE-TO-ONE
        }
    }
}
