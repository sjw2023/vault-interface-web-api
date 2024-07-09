using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public interface IItemService<T> : IBaseService<T>
	{
		T GetByName(string name, VDF.Vault.Currency.Connections.Connection connection);
	}
}
