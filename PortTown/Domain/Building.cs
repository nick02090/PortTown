using Domain.Enums;
using System;

namespace Domain
{
    public abstract class Building : Craftable
    {
        public virtual string Name { get; set; }
        public virtual int Level { get; set; }
        public virtual int Capacity { get; set; }
        public virtual BuildingType BuildingType { get; set; }
        public virtual Town Town { get; set; }
    }
}
