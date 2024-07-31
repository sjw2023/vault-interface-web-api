using Autodesk.Connectivity.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class ItemSearchRequestDTO
	{
		public SrchCond [] SrchCond { get; set; }
		public SrchSort [] SrchSort { get; set; }
		public Boolean LatestReleasedOnly { get; set; }
	}
}
