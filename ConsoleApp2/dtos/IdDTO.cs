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
			Ids = idDTO.Ids;
			Console.WriteLine("Returning IdDTO with copy ctor");
		}
		public IdDTO(long[] ids) { 
			Console.WriteLine("Creating IdDTO with ids[]");
			Ids = ids;
			Console.WriteLine("Returning IdDTO with ids[]");
		}
		public IdDTO()
		{
			Console.WriteLine("Creating IdDTO with default ctor");
			Ids = new long[0];
			Console.WriteLine("Returning IdDTO with default ctor");
		}
		public long[] Ids { get ; set ; }
		}
}
