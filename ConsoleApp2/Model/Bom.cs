using System;
using System.Collections.Generic;

namespace ConsoleApp2.Model
{
    public class Bom : IBaseEntity
    {
        public long Id { get; set; }
        public List<BomNode> Children { get ; set ; }
        public bool IsHighest { get; set; }
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
            this.Children = new List<BomNode>();
        }
        public Bom(bool isHighest) { 
            this.Children = new List<BomNode>();
            this.IsHighest = isHighest;
        }
        public Bom(Bom bom)
        {
            Console.WriteLine("Creating Bom with copy ctor");
            this.Id = bom.Id;
            this.Children = bom.Children;
        }

        public Bom(long id, List<BomNode> children, bool isHighest)
        {
            this.Id = id;
            this.Children = children;
        }
    }
    public class BomNode
    {
        public long Id { get ; set ; }
        public double Quantity { get ; set ; }
        public List<BomNode> Children { get ; set ; }
        public bool IsHighest { get; set; }

        public BomNode()
        {
            this.Children = new List<BomNode>();
        }
        public BomNode(long id)
        {
            this.Id = id;
            this.Children = new List<BomNode>();
        }
    }
}
