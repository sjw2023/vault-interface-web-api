using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public class LogInDecoratorService<T> : IBaseService<T>
	{
		private VDF.Vault.Currency.Connections.Connection _connection;
		public VDF.Vault.Currency.Connections.Connection Connection
		{
			get
			{
				return _connection;
			}
		}
		private VDF.Vault.Results.LogInResult _result;
		private string _server;
		private string _vault;
		private string _user;
		private string _password;
		//TODO: Add AuthenticationFlags
		//TODO: Add ConnectionProperties
		//TODO: Add ConnectionOptions
		private readonly IBaseService<T> _decorated;
		public LogInDecoratorService(IBaseService<T> decorated)
		{
			_decorated = decorated;
		}
		public void LogIn()
		{
			try
			{
				_result = VDF.Vault.Library.ConnectionManager.LogIn("192.168.10.250", "DTcenter", "DTcenter", "1234"
						//"192.168.10.250", "DTcenter", "joowon.suh@woosungautocon.com", "R-6qEbT#*nrJLZp"
						, VDF.Vault.Currency.Connections.AuthenticationFlags.Standard, null);
				if (!_result.Success)
				{
					foreach (var key in _result.ErrorMessages.Keys)
					{
						Console.WriteLine(_result.ErrorMessages[key]);
					}
					throw new Exception("Login Failed");
				}
				else
				{
					Console.WriteLine("Login Success");
				}
				_connection = _result.Connection;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private void LogOut()
		{
			VDF.Vault.Library.ConnectionManager.LogOut(_connection);
		}

		public void Add(T entity, Connection connection)
		{
			LogIn();
			_decorated.Add(entity, _connection);
			LogOut();
		}

		public void Delete(T entity, Connection connection)
		{
			LogIn();
			_decorated.Delete(entity, _connection);
			LogOut();
		}

		public void Update(T entity, Connection connection)
		{
				LogIn();
			_decorated.Update(entity, _connection);
			LogOut();
		}

		public T GetById(long id, Connection connection)
		{
			LogIn();
			var entity = _decorated.GetById(id, _connection);
			LogOut();
			return entity;
		}

		public IEnumerable<T> GetAll(Connection connection)
		{
			LogIn();
			var entities = _decorated.GetAll(_connection);
			LogOut();
			return entities;
		}
	}
}
