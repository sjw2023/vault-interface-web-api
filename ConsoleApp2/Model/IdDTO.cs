using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class IdDTO
	{
		private long[] _ids;
		public long[] Ids { get { return _ids; } set { _ids = value; } }
	}
}
