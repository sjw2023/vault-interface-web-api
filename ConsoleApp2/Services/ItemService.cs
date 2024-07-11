using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp2.Services;
using MyItem = ConsoleApp2.Model.Item;
using VaultItem = Autodesk.Connectivity.WebServices.Item;
using VDF = Autodesk.DataManagement.Client.Framework;
using Autodesk.Connectivity.WebServices;
using ConsoleApp2.Util;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Model;


[CustomExceptionFilter]
public class ItemService<T> : IItemService<T>, IBaseService<T> where T : ItemDTO 
{
	private string _entityClassId = VDF.Vault.Currency.Entities.EntityClassIds.Items;
	private Cat[] _categories;
	private HelperMethod _helperMethod = new HelperMethod();

	public void Add(T entity, VDF.Vault.Currency.Connections.Connection connection)
	{
		try{ 
			long catId = GetCategoryIdByName("ITEM", connection);
			Autodesk.Connectivity.WebServices.Item item = connection.WebServiceManager.ItemService.AddItemRevision(catId);

			//TODO : Create Numbering scheme service or helper method. Show the numbering scheme list and select the scheme
			// set item numbering scheme
			NumSchm numSchm1 = new NumSchm();
			NumSchm[] numSchms = connection.WebServiceManager.NumberingService.GetNumberingSchemes("ITEM", NumSchmType.All);
			foreach (NumSchm numSchm in numSchms)
			{
				if (numSchm.SchmID == 8)
				{
					numSchm1 = numSchm;
				}
			}
			item.NumSchmId = numSchm1.SchmID;



			string[] newItemNum = new string[] { "ItemAddTest" };
			StringArray[] fieldInputs = new StringArray[1];
			StringArray tempArr = new StringArray();
			tempArr.Items = newItemNum;
			fieldInputs[0] = tempArr;

			ProductRestric[] productRestrics;

			//Update Item number to Vault
			ItemNum[] itemNums = connection.WebServiceManager.ItemService.AddItemNumbers(new long[] { item.MasterId }, new long[] { item.NumSchmId }, fieldInputs, out productRestrics);

			//Set new item values
			item.ItemNum = itemNums[0].ItemNum1;
			item.Title = "11111";
			item.VerNum = 10;
			item.RevNum = "A";


			///set BOM information
			ItemAssocParam[] itemAssocParams = new ItemAssocParam[1];
			itemAssocParams[0] = new ItemAssocParam();
			//itemAssocParams[0].BOMOrder = newest;
			itemAssocParams[0].CldItemID = 40581;
			//itemAssocParams[0].Id = 1;
			itemAssocParams[0].Quant = 1;
			itemAssocParams[0].EditAct = BOMEditAction.Add;
			//itemAssocParams[0].InstCount = 1;
			//itemAssocParams[0].IsIncluded = true;
			itemAssocParams[0].IsStatic = true;
			//itemAssocParams[0].PositionNum = "position number";
			//itemAssocParams[0].UnitID = new;
			//itemAssocParams[0].UnitSize = 1;


			var revs = connection.WebServiceManager.ItemService.UpdateItemBOMAssociations( item.Id, itemAssocParams, BOMViewEditOptions.ReturnBOMFragmentsOnEdits);
			//connection.WebServiceManager.ItemService.UpdateAndCommitItems(revs);

			//Update Item values to Vault
			connection.WebServiceManager.ItemService.UpdateAndCommitItems(new Autodesk.Connectivity.WebServices.Item[] { item });
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Source);
			Console.WriteLine($"Failed to add item {ex.Message}");
			Console.WriteLine(ex.StackTrace);
			Console.WriteLine(ex.InnerException);
		}
	}

	/// <summary>
	/// TODO : Implement Category service
	/// </summary>
	/// <param name="entityClassId"></param>
	/// <param name="connection"></param>
	/// <returns></returns>
	private long GetCategoryIdByName(string entityClassId, VDF.Vault.Currency.Connections.Connection connection)
	{
		if (_categories == null) { 
			_categories = connection.WebServiceManager.CategoryService.GetCategoriesByEntityClassId(entityClassId, true);
		}
		long catId = -1;
		foreach (Cat category in _categories)
		{
			Console.WriteLine($"{category.SysName} | {category.Name} | {category.Id} | {category.EntClassIdArray}");
			if (category.Name == "일반")
				catId = category.Id;
		}
		return catId;
	}

	public void Delete(T entity, VDF.Vault.Currency.Connections.Connection connection)
	{
		throw new NotImplementedException();
	}

	public void Update(T entity, VDF.Vault.Currency.Connections.Connection connection)
	{
		throw new NotImplementedException();
		//ItemRevision itemRevision = connection.WebServiceManager.ItemService.GetItemsByFileIds(entity.Id);
		//itemRevision[0].Name = entity.Name;


		// only can change name, detail, comment, itemTypeID, and units
		connection.WebServiceManager.ItemService.UpdateAndCommitItems(new VaultItem[] {  });
	}

	public T GetById(long id, VDF.Vault.Currency.Connections.Connection connection)
	{
		return GetAll( new long[] { id }, connection);
	}
	
	/// <summary>
	/// Get all items and its properties values
	/// TODO : Refactor this method can get thumbnail
	///		: Refactor more efficient way to get all items
	/// </summary>
	/// <param name="connection"></param>
	/// <returns></returns>
	public T GetAll(long[] ids, VDF.Vault.Currency.Connections.Connection connection)
	{
		//TODO : create server context and store the Vault Configuration information
		//ServerCfg serverCfg = connection.WebServiceManager.AdminService.GetServerConfiguration();
		//TODO : Updating _items is not done with this code. Refactor to check items update status.

		List<ConsoleApp2.Model.Item> itemsToRet = new List<ConsoleApp2.Model.Item>();
		List<VaultItem> items = new List<VaultItem>();
		if (ids == null) { 
			items = _helperMethod.GetAllItems(connection);
		}
		else {
			items = connection.WebServiceManager.ItemService.GetItemsByIds( ids ).ToList();
		}
		var masterIds = from item in items
						select item.MasterId;
		var properties = _helperMethod.GetPropInst(connection, masterIds.ToArray(), _entityClassId);
		foreach ( var item in items) { 

			var itemTmp = (ConsoleApp2.Model.Item)Activator.CreateInstance(typeof(ConsoleApp2.Model.Item));
			//T itemTmp = (T)Activator.CreateInstance(typeof(T));
			itemTmp.Id = item.Id;
			itemTmp.MasterId = item.MasterId;
			itemTmp.Name = item.ItemNum;
			itemTmp.PropInstDTOs = properties.Where(prop => prop.Id == item.Id).ToList();
			itemsToRet.Add(itemTmp);
		}
		return (T) new ItemDTO(new ItemDTO.ItemResponseDTO( itemsToRet.ToArray() ));
	}
	public T GetByName(string name, Connection connection )
	{
		var toRet = (ConsoleApp2.Model.Item)Activator.CreateInstance(typeof(ConsoleApp2.Model.Item));
		var latestItem = connection.WebServiceManager.ItemService.GetLatestItemByItemNumber(name);
		toRet.Id = latestItem.Id;
		toRet.MasterId = latestItem.MasterId;
		toRet.Name = latestItem.ItemNum;
		var properties = _helperMethod.GetPropInst(connection, new long[] { latestItem.MasterId },_entityClassId);
		toRet.PropInstDTOs = properties.Where(prop => prop.Id == latestItem.Id).ToList();
		return (T) new ItemDTO(new ItemDTO.ItemResponseDTO( new ConsoleApp2.Model.Item[] { toRet }));
	}
}
