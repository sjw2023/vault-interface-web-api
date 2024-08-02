namespace ConsoleApp2.Model
{
    public class Bom : IBaseEntity
    {
        public long Id { get; set; }
        private List<BomNode> _children;
        public List<BomNode> Children
        {
            get { return _children; }
            set { _children = value; }
        }
        public BomNode FindBom(List<BomNode> nodes, long target)
        {
            if (nodes == null)
                return null;
            foreach (var child in nodes)
            {
                if (target == child.Id)
                {
                    return child;
                }
                else
                {
                    var result = FindBom(child.Children, target);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
        public Bom()
        {
            this._children = new List<BomNode>();
        }
        public Bom(Bom bom)
        {
            Console.WriteLine("Creating Bom with copy ctor");
            Id = bom.Id;
            Children = bom.Children;
        }

        public Bom(long id, List<BomNode> children)
        {
            Id = id;
            Children = children;
        }
            }
    public class BomNode
    {
        private long _id;
        public long Id { get { return _id; } set { _id = value; } }
        private double _quantity;
        public double Quantity { get { return _quantity; } set { _quantity = value; } }
        private List<BomNode> _children;
        public List<BomNode> Children
        {
            get { return _children; }
            set { _children = value; }
        }
        public BomNode()
        {
            this._children = new List<BomNode>();
        }
        public BomNode(long id)
        {
            this._id = id;
            this._children = new List<BomNode>();
        }
    }
}
