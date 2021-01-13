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

        // Reference to parent
        public virtual Craftable ParentCraftable { get; set; }
        // References to children
        public virtual Storage ChildStorage { get; set; }
        public virtual Silo ChildSilo { get; set; }
        public virtual ProductionBuilding ChildProductionBuilding { get; set; }

        public Building()
        {
            // Craftable
            CraftableType = CraftableType.Building;

            // TODO: Serialize Name
            Level = 1;
            // TODO: Serialize Capacity
        }

        public Building(Craftable parentCraftable) : base()
        {
            SetParentCraftable(parentCraftable);
        }

        public virtual void SetParentCraftable(Craftable parentCraftable)
        {
            ParentCraftable = parentCraftable;
            RequiredResources = parentCraftable.RequiredResources;
            TimeToBuild = parentCraftable.TimeToBuild;
            TimeUntilCrafted = parentCraftable.TimeUntilCrafted;
            IsFinishedCrafting = parentCraftable.IsFinishedCrafting;
        }
    }
}
