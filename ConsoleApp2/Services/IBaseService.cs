using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public interface IBaseService<T>
	{
		void Add(T entity, VDF.Vault.Currency.Connections.Connection connection);
		void Delete(T entity, VDF.Vault.Currency.Connections.Connection connection);
		void Update(T entity, VDF.Vault.Currency.Connections.Connection connection);
		T GetById(long id, VDF.Vault.Currency.Connections.Connection connection);
		IEnumerable<T> GetAll(long[] ids, VDF.Vault.Currency.Connections.Connection connection);
	}
}
