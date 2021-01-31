using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApp.Models.Resources
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