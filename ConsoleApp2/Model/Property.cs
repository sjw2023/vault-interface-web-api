using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class Property : IBaseEntity
	{
		// PropDef
		// PropInst
		//

		public long Id { get; set; }
		public string Name { get; set; }
		public string EntityName { get; set; }
		public Property()
		{
		}
		public Property(long id, string name, string entityName)
		{
			Id = id;
			Name = name;
			EntityName = entityName;
		}
	}
}
