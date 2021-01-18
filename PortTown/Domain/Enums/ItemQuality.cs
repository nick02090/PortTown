using System.ComponentModel;

namespace Domain.Enums
{
    public enum ItemQuality
    {
        [Description("Poor")]
        Poor,
        [Description("Good")]
        Good,
        [Description("Excellent")]
        Excellent,
        [Description("Masterwork")]
        Masterwork
    }
}
