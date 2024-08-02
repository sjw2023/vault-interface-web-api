namespace ConsoleApp2.Model
{
    public class Item : IBaseEntity
    {
        public long Id { get; set;} 
        public string Name { get; set; }
        public long MasterId { get; set; }
        public List<FileAssocDTO> FileAssocDTOs { get; set; } 
        public List<PropInstDTO> PropInstDTOs { get ; set ; }
        public Item() { }
        public Item(Item item)
        {
            Console.WriteLine("Creating Item with copy ctor");
            Id = item.Id;
            Name = item.Name;
            MasterId = item.MasterId;
            PropInstDTOs = item.PropInstDTOs;
        }
        public Item(long id, string name, long masterId, List<PropInstDTO> propInstDTOs)
        {
            Console.WriteLine("Creating Item with id, name, masterId, boms, propInstDTOs");
            Id = id;
            Name = name;
            MasterId = masterId;
            PropInstDTOs = propInstDTOs;
        }
        // Additional constructor for convinience
        public Item(Autodesk.Connectivity.WebServices.Item vaultItem)
        {
            this.Name = vaultItem.ItemNum;
            this.Id = vaultItem.Id;
            this.MasterId = vaultItem.MasterId;
        }
    }
}
