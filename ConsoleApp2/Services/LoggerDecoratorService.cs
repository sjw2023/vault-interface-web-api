using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Results
{
	public class LoggerDecoratorService<T> : IBaseService<T>, IItemService<T>
	{
		private readonly IBaseService<T> _decorated;
		private readonly IItemService<T> _decoratedItem;
		private Connection _connection;
		public LoggerDecoratorService(IBaseService<T> decorated)
		{
			_decorated = decorated;
			_decoratedItem = decorated as IItemService<T>;
		}
		private void Log(string msg, object arg = null)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(msg, arg);
			Console.ResetColor();
		}
		public void Add(T entity, Connection connection)
		{
			Log("Adding entity: {0}", entity);
			_decorated.Add(entity, connection);
			Log("Entity added: {0}", entity);
		}

		public void Delete(T entity, Connection connection)
		{
			Log("Deleting entity: {0}", entity);
			_decorated.Delete(entity, connection);
			Log("Entity deleted: {0}", entity);
		}

		public void Update(T entity, Connection connection)
		{
			Log("Updating entity: {0}", entity);
			_decorated.Update(entity, connection);
			Log("Entity updated: {0}", entity);
		}

		public T GetById(long id, Connection connection)
		{
			Log("Getting entity by id: {0}", id);
			var entity = _decorated.GetById(id, connection);
			Log("Entity found: {0}", entity);
			return entity;
		}

		public IEnumerable<T> GetAll(long[] ids, Connection connection)
		{
			Log("Getting all entities");
			var entities = _decorated.GetAll(ids, connection);
			Log("All entities found: {0}", entities.Count());
			return entities;
		}

		public T GetByName(string nam1e, Connection connection)
		{
			Log("Getting entity by name: {0}", nam1e);
			var entity = _decoratedItem.GetByName(nam1e, connection);
			Log("Entity found: {0}", entity);
			return entity;
		}
	}
}
