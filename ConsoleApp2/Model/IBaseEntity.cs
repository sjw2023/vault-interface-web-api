using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public interface IBaseEntity
	{
		long Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
	}
}
