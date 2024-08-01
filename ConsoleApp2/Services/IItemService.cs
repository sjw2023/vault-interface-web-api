using System.Collections.Generic;
using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Model;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public interface IItemService<T> : IBaseService<T>
	{
		T GetByName(string name, VDF.Vault.Currency.Connections.Connection connection);
		T GetBySchCond(
			SrchCond[] srchCond,
			SrchSort[] sortConditions,
			bool bRequestLatestOnly,
			ref string bookmark,
			out SrchStatus searchstatus,
			VDF.Vault.Currency.Connections.Connection connection);
		T GetByDate(List<string> dates, Connection connection);
	}
}
