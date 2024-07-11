using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

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
					var result = FindBom(child.Children, target);  // 재귀 호출의 반환값을 저장
					if (result != null)  // 유효한 값이 반환되면 그것을 반환
						return result;
				}
			}
			return null;  // 아무 것도 찾지 못했을 때
		}
		public Bom()
		{
			this._children = new List<BomNode>();  // null 대신 빈 리스트로 초기화
		}
		public Bom(long id, List<BomNode> children)
		{
			Id = id;
			Children = children;
		}
		public Bom(Bom bom) { 
			Console.WriteLine("Creating Bom with copy ctor");
			Id = bom.Id;
			Children = bom.Children;
		}
	}
	public class BomNode {
		private long _id;
		public long Id { get { return _id; } set { _id = value; } }
		private List<BomNode> _children;
		public List<BomNode> Children
		{
			get { return _children; }
			set { _children = value; }
		}
		public BomNode() {
			this._children = new List<BomNode>();
		}
		public BomNode(long id)
		{
			this._id = id;
			this._children = new List<BomNode>();
		}
	}
}
