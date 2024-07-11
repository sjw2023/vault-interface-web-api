using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Services;
using DevExpress.XtraBars.Docking2010.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Results
{
	internal class Service<T> : IBaseService<T>
	{
		public void Add(T entity)
		{
			throw new NotImplementedException();
		}

		public void Add(T entity, Connection connection)
		{
			throw new NotImplementedException();
		}

		public void Delete(T entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(T entity, Connection connection)
		{
			throw new NotImplementedException();
		}

		public T GetAll()
		{
			throw new NotImplementedException();
		}

		public T GetAll(long[] ids, Connection connection)
		{
			throw new NotImplementedException();
		}

		public T GetById(long id)
		{
			throw new NotImplementedException();
		}

		public T GetById(long id, Connection connection)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity)
		{
			throw new NotImplementedException();
		}

		public void Update(T entity, Connection connection)
		{
			throw new NotImplementedException();
		}
	}
}
