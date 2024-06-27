using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	internal class Bom : IBaseEntity
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public Bom()
		{
		}
	}
}
