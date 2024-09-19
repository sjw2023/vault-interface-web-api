using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class PropertyRequestDTO
	{
		private IdDTO _idDTO;
		public IdDTO m_IdDTO { get { return _idDTO; } set { _idDTO = value; } }
		public class IdDTO {
			private long[] _ids;
			public long[] Ids { get { return _ids; } set { _ids = value; } }
		}
	}
}
