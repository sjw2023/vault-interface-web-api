﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp2.Services;
using MyItem = ConsoleApp2.Model.Item;
using VaultItem = Autodesk.Connectivity.WebServices.Item;
using VDF = Autodesk.DataManagement.Client.Framework;
using Autodesk.Connectivity.WebServices;
using ConsoleApp2.Util;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Model;
using Connection = Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections.Connection;


[CustomExceptionFilter]
public class ItemService<T> : IItemService<T> where T : ItemDTO
{
    private string _entityClassId = VDF.Vault.Currency.Entities.EntityClassIds.Items;
    private Cat[] _categories;
    private HelperMethod _helperMethod = new HelperMethod();

    public void Add(T entity, Connection connection)
    {
        throw new NotImplementedException();
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


        // set BOM information
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


        var revs = connection.WebServiceManager.ItemService.UpdateItemBOMAssociations(item.Id, itemAssocParams, BOMViewEditOptions.ReturnBOMFragmentsOnEdits);
        //connection.WebServiceManager.ItemService.UpdateAndCommitItems(revs);

        //Update Item values to Vault
        connection.WebServiceManager.ItemService.UpdateAndCommitItems(new Autodesk.Connectivity.WebServices.Item[] { item });
    }

    /// <summary>
    /// TODO : Implement Category service
    /// </summary>
    /// <param name="entityClassId"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    private long GetCategoryIdByName(string entityClassId, Connection connection)
    {
        if (_categories == null)
        {
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

    public void Delete(T entity, Connection connection)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity, Connection connection)
    {
        throw new NotImplementedException();
        /*
		ItemRevision itemRevision = connection.WebServiceManager.ItemService.GetItemsByFileIds(entity.Id);
		itemRevision[0].Name = entity.Name;
		change name, detail, comment, itemTypeID, and units
		*/
        connection.WebServiceManager.ItemService.UpdateAndCommitItems(new VaultItem[] { });
    }

    /// <summary>
    /// Delegate to GetALL to fine Item by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    public T GetById(long id, Connection connection)
    {
        return GetAll(new[] { id }, connection);
    }

    /// <summary>
    /// Get all items and its properties values
    /// TODO : Refactor this method can get thumbnail
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="connection"></param>
    /// <returns></returns>
    public T GetAll(long[] ids, Connection connection)
    {
        //TODO : create server context and store the Vault Configuration information
        //ServerCfg serverCfg = connection.WebServiceManager.AdminService.GetServerConfiguration();
        //TODO : Updating _items is not done with this code. Refactor to check items update status.
        bool isAll = ids == null;
        List<ConsoleApp2.Model.Item> itemsToRet = new List<ConsoleApp2.Model.Item>();
        List<VaultItem> items = new List<VaultItem>();
        if (isAll)
        {
            Console.WriteLine("GetAll null : " + items.Count);
            items = _helperMethod.GetAllItems(connection);
        }
        else
        {
            items = connection.WebServiceManager.ItemService.GetItemsByIds(ids).ToList();
            Console.WriteLine("GetAll : " + items.Count);
        }
        var masterIds = from item in items
                        select item.MasterId;
        var itemIds = from item in items
                      select item.Id;
        ItemFileAssoc[] itemFileAssoc = connection.WebServiceManager.ItemService.GetItemFileAssociationsByItemIds(itemIds.ToArray(), ItemFileLnkTypOpt.Primary);
        List<PropInstDTO> properties = new List<PropInstDTO>();
        if (!isAll)
        {
            properties.AddRange(_helperMethod.GetPropInst(connection, masterIds.ToArray(), _entityClassId));
        }
        foreach (var item in items)
        {
            List<FileAssocDTO> fileAssocDTOs = new List<FileAssocDTO>();
            fileAssocDTOs.AddRange(itemFileAssoc.Where(assoc => assoc.ParItemId == item.Id).Select(assoc => new FileAssocDTO(assoc)));
            if (isAll)
            {
                var itemTmp = new ConsoleApp2.Model.Item
                {
                    Id = item.Id,
                    MasterId = item.MasterId,
                    Name = item.ItemNum,
                    //add each files
                    //FileAssocDTOs = fileAssocDTOs,
                    //PropInstDTOs = properties.Where(prop => prop.Id == item.Id).ToList()
                };
                itemsToRet.Add(itemTmp);
            }
            else
            {
                var itemTmp = new ConsoleApp2.Model.Item
                {
                    Id = item.Id,
                    MasterId = item.MasterId,
                    Name = item.ItemNum,
                    //add each files
                    FileAssocDTOs = fileAssocDTOs,
                    PropInstDTOs = properties.Where(prop => prop.Id == item.Id).ToList()
                };
                itemsToRet.Add(itemTmp);
            }
        }
        Console.WriteLine("GetAll : " + itemsToRet.Count);
        return (T)new ItemDTO(new ItemDTO.ItemResponseDTO(itemsToRet.ToArray()));
    }

    public T GetByName(string name, Connection connection)
    {
        var latestItem = connection.WebServiceManager.ItemService.GetLatestItemByItemNumber(name);
        var properties = _helperMethod.GetPropInst(connection, new[] { latestItem.MasterId }, _entityClassId);
        var toRet = new ConsoleApp2.Model.Item
        {
            Id = latestItem.Id,
            MasterId = latestItem.MasterId,
            Name = latestItem.ItemNum,
            PropInstDTOs = properties.Where(prop => prop.Id == latestItem.Id).ToList()
        };
        return (T)new ItemDTO(new ItemDTO.ItemResponseDTO(new[] { toRet }));
    }

    public T GetBySchCond(SrchCond[] srchCond, SrchSort[] sortConditions, bool bRequestLatestOnly, ref string bookmark, out SrchStatus searchStatus, Connection connection)
    {
        SrchStatus tempStatus = null;
        List<VaultItem> items = new List<VaultItem>();
        while (tempStatus == null || tempStatus.TotalHits > items.Count)
        {
            items.AddRange(connection.WebServiceManager.ItemService.FindItemRevisionsBySearchConditions(srchCond, null, true, ref bookmark, out tempStatus));
            Console.WriteLine("GetBySchCond : " + tempStatus.TotalHits);
        }
        List<MyItem> itemsToRet = new List<MyItem>();
        foreach (var item in items)
        {
            itemsToRet.Add(new MyItem(item));
        }
        searchStatus = tempStatus;
        return (T)new ItemDTO(new ItemDTO.ItemResponseDTO(itemsToRet.ToArray()));
    }

    public T GetByDate(string date = null, Connection connection = null)
    {
        List<SrchCond> srchConds = new List<SrchCond>();
        if (date != null)
        {
            SrchCond temp = new SrchCond();
            temp.PropTyp = PropertySearchType.SingleProperty;
            temp.SrchOper = 7L;
            temp.SrchRule = SearchRuleType.May;
            temp.SrchTxt = date;
            temp.PropDefId = 25;
            srchConds.Add(temp);

            SrchCond temp2 = new SrchCond();
            Console.WriteLine($"{date}");
            temp2.PropTyp = PropertySearchType.SingleProperty;
            temp2.SrchOper = 7;
            temp2.SrchRule = SearchRuleType.May;
            temp2.SrchTxt = date;
            temp2.PropDefId = 11;
            srchConds.Add(temp2);
        }
        string bookmark = null;
        SrchStatus searchStatus = null;
        return GetBySchCond(srchConds.ToArray(), null, true, ref bookmark, out searchStatus, connection);
    }
}
