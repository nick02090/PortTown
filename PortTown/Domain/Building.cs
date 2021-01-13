using Domain.Enums;

namespace Domain
{
    public class Building : Craftable
    {
        public virtual string Name { get; set; }
        public virtual int Level { get; set; }
        public virtual int Capacity { get; set; }
        public virtual BuildingType BuildingType { get; set; }
        public virtual Town Town { get; set; }
        public virtual Craftable ParentCraftable { get; set; }
        public virtual Storage ChildStorage { get; set; }
        public virtual Silo ChildSilo { get; set; }
        public virtual ProductionBuilding ChildProductionBuilding { get; set; }

        public Building()
        {
            // Craftable
            CraftableType = CraftableType.Building;

            // SERIALIZE: Name
            Level = 1;
            // SERIALIZE: Capacity
        }
    }
}
