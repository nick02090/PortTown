using System.ComponentModel;

namespace Domain.Enums
{
    public enum ResourceType
    {
        [Description("Stone")]
        Stone,
        [Description("Wood")]
        Wood,
        [Description("Gold")]
        Gold,
        [Description("Food")]
        Food,
        [Description("Iron")]
        Iron,
        [Description("Coal")]
        Coal
    }
}
