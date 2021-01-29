using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Enums
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
