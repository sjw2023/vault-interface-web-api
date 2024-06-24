using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp2.Services;
using MyItem = ConsoleApp2.Model.Item;
using ConsoleApp2.Model;
using VaultItem = Autodesk.Connectivity.WebServices.Item;

using VDF = Autodesk.DataManagement.Client.Framework;
using Autodesk.Connectivity.WebServices;

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
		VDF.Vault.Results.LogInResult result = VDF.Vault.Library.ConnectionManager.LogIn("192.168.10.250", "DTcenter", "DTcenter", "1234"
						//"192.168.10.250", "DTcenter", "joowon.suh@woosungautocon.com", "R-6qEbT#*nrJLZp"
						, VDF.Vault.Currency.Connections.AuthenticationFlags.Standard, null);
		VDF.Vault.Currency.Connections.Connection connection = result.Connection;

		if (!result.Success)
		{
			foreach (var key in result.ErrorMessages.Keys)
			{
				Console.WriteLine(result.ErrorMessages[key]);
			}
		}

		_items = new List<VaultItem>();

		string bookmark = null;
		SrchStatus status = null;
		while (status == null || status.TotalHits > _items.Count)
		{
			_items.AddRange(connection.WebServiceManager.ItemService.FindItemRevisionsBySearchConditions(null, null, true, ref bookmark, out status));
		}
		int index = 1;
		foreach ( VaultItem item in _items)
		{
			Console.WriteLine($"{index++} | {item.ItemNum} | {item.MasterId} | {item.RevNum} | {item.Id} ");
		}
		VDF.Vault.Library.ConnectionManager.LogOut(connection);
		return _items.Cast<T>();
	}

	public void nothing() { }
}
