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
		private List<FileAssocDTO> _fileAssocDTOs;
		public List<FileAssocDTO> FileAssocDTOs
		{
			get { return _fileAssocDTOs; }
			set { _fileAssocDTOs = value; }
		}
		private List<PropInstDTO> _propInstDTOs;
		public List<PropInstDTO> PropInstDTOs
		{
			get { return _propInstDTOs; }
			set { _propInstDTOs = value; }
		}
		public Item() { }
		public Item(Item item) { 
			Console.WriteLine("Creating Item with copy ctor");
			Id = item.Id;
			Name = item.Name;
			MasterId = item.MasterId;
			PropInstDTOs = item.PropInstDTOs;
		}
		public Item(long id, string name, long masterId, List<PropInstDTO> propInstDTOs)
		{
			Console.WriteLine("Creating Item with id, name, masterId, boms, propInstDTOs");
			Id = id;
			Name = name;
			MasterId = masterId;
			PropInstDTOs = propInstDTOs;
		}
		public Item(Autodesk.Connectivity.WebServices.Item vaultItem) { 
			this.Name = vaultItem.ItemNum;
			this.Id = vaultItem.Id;
			this.MasterId = vaultItem.MasterId;
		}
	}
}
