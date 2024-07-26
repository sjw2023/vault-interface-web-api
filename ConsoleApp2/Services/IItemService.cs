using Autodesk.Connectivity.WebServices;
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
	}
}
