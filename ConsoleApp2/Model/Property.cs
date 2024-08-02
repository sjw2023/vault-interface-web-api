namespace ConsoleApp2.Model
{
    public class Property : IBaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<String> AssociatedEntityName { get; set; }
        public Property()
        {
            AssociatedEntityName = new List<string>();
        }
        public Property(long id, string name, List<String> entityName)
        {
            Id = id;
            Name = name;
            AssociatedEntityName = entityName;
        }
    }
}
