using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp2.Services;
using MyItem = ConsoleApp2.Model.Item;
using ConsoleApp2.Model;
using VaultItem = Autodesk.Connectivity.WebServices.Item;

using VDF = Autodesk.DataManagement.Client.Framework;
using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;

public class ItemService<T> : IBaseService<T> where T : IBaseEntity
{
	private List<VaultItem> _items;

	public void Add(T entity)
	{
		// Add logic to add the item to the data store
		Console.WriteLine(entity.Id);
		//_items.Add(entity);
	}

	public void Delete(T entity)
	{
		// Add logic to delete the item from the data store
		//_items.Remove(entity);
	}

	public void Update(T entity)
	{
		// Add logic to update the item in the data store
		// This is a simplistic approach and might need to be adjusted based on your data store mechanism
		var item = GetById(entity.Id);
		if (item != null)
		{
			//_items.Remove(item);
			//_items.Add(entity);
		}
	}

	public T GetById(long id)
	{
		// Retrieve the item by its ID from the data store
		//return _items.FirstOrDefault(i => i.Id == id);
		return default(T);
	}

	public IEnumerable<T> GetAll()
	{
		throw new NotImplementedException();
	}

	public void nothing() { }

	public void Add(T entity, Connection connection)
	{
		throw new NotImplementedException();
	}

	public void Delete(T entity, Connection connection)
	{
		throw new NotImplementedException();
	}

	public void Update(T entity, Connection connection)
	{
		throw new NotImplementedException();
	}

	public T GetById(long id, Connection connection)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<T> GetAll(Connection connection)
	{
		_items = new List<VaultItem>();
		string bookmark = null;
		SrchStatus status = null;
		while (status == null || status.TotalHits > _items.Count)
		{
			_items.AddRange(connection.WebServiceManager.ItemService.FindItemRevisionsBySearchConditions(null, null, true, ref bookmark, out status));
		}
		return _items.Cast<T>();
	}
}
