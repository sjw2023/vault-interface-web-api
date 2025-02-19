using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class PropInstDTO : IBaseEntity
	{
		private long _id;
		public long Id { get { return _id; } set {_id=value; } }
		private long _propDefId;
		public long PropDefId { get; set; }
		private string _name;
		public string Name { get; set; }
		private object _value;
		public object Value { get; set; }
	}
}
