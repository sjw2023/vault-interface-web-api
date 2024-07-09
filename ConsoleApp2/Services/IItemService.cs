using ConsoleApp2.Model;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public interface IItemService<T> : IBaseService<T>
	{
		T GetByName(string name, VDF.Vault.Currency.Connections.Connection connection);
	}
}
