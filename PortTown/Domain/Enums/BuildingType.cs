using System.ComponentModel;

namespace Domain.Enums
{
    public enum BuildingType
    {
        [Description("Production")]
        Production,
        [Description("Storage")]
        Storage,
        [Description("Silo")]
        Silo
    }
}
