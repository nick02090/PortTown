using System.Collections.Generic;
using Domain.Enums;

namespace Domain
{
    public class Storage : Building
    {
        public Dictionary<ResourceType, float> StoredResources { get; set; }
    }
}
