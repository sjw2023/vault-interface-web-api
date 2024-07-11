using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class IdDTO {
		public IdDTO(IdDTO idDTO) {
			Console.WriteLine("Creating IdDTO with copy ctor");
			_ids = idDTO._ids;
		}
		public IdDTO(long[] ids) { 
			Console.WriteLine("Creating IdDTO with ids");
			_ids = ids;
		}
		public IdDTO()
		{
		}
		private long[] _ids;
			public long[] Ids { get { return _ids; } set { _ids = value; } }
		}

}
