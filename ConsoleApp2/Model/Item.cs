using Autodesk.Connectivity.WebServices;
using ConsoleApp2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class Item : IBaseEntity
	{
		private long _id;
		public long Id { get { return _id; } set { _id = value; } }
		private string _name;
		public string Name { get { return _name; } set { _name = value; } }
		private long _masterId;
		public long MasterId
		{
			get { return _masterId; }
			set { _masterId = value; }
		}
		private List<Bom> _boms;
		public List<Bom> Boms
		{
			get { return _boms; }
			set { _boms = value; }
		}
		private List<PropInstDTO> _propInstDTOs;
		public List<PropInstDTO> PropInstDTOs
		{
			get { return _propInstDTOs; }
			set { _propInstDTOs = value; }
		}
		public Item() { }
	}
}
