namespace poc_specification_pattern.Shared
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
    }
}