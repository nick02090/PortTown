using System.Collections.Generic;
using Domain.Enums;

namespace Domain
{
    public class Storage : Building
    {
        public virtual IDictionary<ResourceType, int> StoredResources { get; set; }
    }
}
