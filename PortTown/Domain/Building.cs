namespace Domain
{
    public abstract class Building : Craftable
    {
        public int Level { get; set; }
        public int Capacity { get; set; }
    }
}
