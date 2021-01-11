namespace Domain
{
    public class Item : Craftable
    {
        public virtual string Name { get; set; }
        public virtual int Value { get; set; }
    }
}
