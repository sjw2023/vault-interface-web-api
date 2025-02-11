﻿using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Model;
using ConsoleApp2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
    [CustomExceptionFilter]
    public class BomService<T> : IBaseService<T> where T : ItemDTO
    {
        private HelperMethod _helperMethod = new HelperMethod();
        private IEnumerable<Autodesk.Connectivity.WebServices.Item> _items;

        public void Add(T entity, Connection connection)
        {
            throw new NotImplementedException();
            //try {
            //	Autodesk.Connectivity.WebServices.Item newest = connection.WebServiceManager.ItemService.GetLatestItemByItemMasterId(entity.Id);
            //	var revs = connection.WebServiceManager.ItemService.EditItems( new long[] { newest.RevId });
            //	var bom2 = connection.WebServiceManager.ItemService.GetItemBOMByItemIdAndDate(31581, DateTime.MinValue, BOMTyp.Latest, BOMViewEditOptions.ReturnBOMFragmentsOnEdits);

            //	var itemassoc = bom2.ItemAssocArray.FirstOrDefault(x => x.CldItemMasterID == 31581);
            //	ItemAssocParam[] itemAssocParams = new ItemAssocParam[1];
            //	itemAssocParams[0] = new ItemAssocParam();
            //	//itemAssocParams[0].BOMOrder = newest;
            //	//itemAssocParams[0].CldItemID = entity.ChildId;
            //	itemAssocParams[0].Id = itemassoc.Id;
            //	//itemAssocParams[0].Quant = 1;
            //	itemAssocParams[0].EditAct = BOMEditAction.Update;
            //	//itemAssocParams[0].InstCount = 1;
            //	//itemAssocParams[0].IsIncluded = true;
            //	//itemAssocParams[0].IsStatic = true;
            //	//itemAssocParams[0].PositionNum = "position number";
            //	//itemAssocParams[0].UnitID = newest.UnitId;
            //	//itemAssocParams[0].UnitSize = 1;



            //	connection.WebServiceManager.ItemService.UpdateItemBOMAssociations( entity.Id, itemAssocParams, BOMViewEditOptions.ReturnBOMFragmentsOnEdits);
            //	connection.WebServiceManager.ItemService.UpdateAndCommitItems(revs);
            //}
            //catch (Exception e)
            //{
            //	connection.WebServiceManager.ItemService.UndoEditItems(new long[] {  });
            //	Console.WriteLine(e.Message);
            //	//Console.WriteLine(e.StackTrace);
            //	//Console.WriteLine(e.InnerException);
            //	//Console.WriteLine(e.Source);
            //	//Console.WriteLine(e.TargetSite);
            //	//Console.WriteLine(e.Data);
            //	//Console.WriteLine(e.HelpLink);
            //	//Console.WriteLine(e.HResult);
            //}
        }

        public void Delete(T entity, Connection connection)
        {
            //ItemAssocParam[] itemAssocParams = new ItemAssocParam[1];
            //itemAssocParams[0] = new ItemAssocParam();
            //itemAssocParams[0].BOMOrder = 0;
            //itemAssocParams[0].CldItemID = entity.Id;
            //itemAssocParams[0].Id = 1;
            //itemAssocParams[0].Quant = 1;
            //itemAssocParams[0].EditAct = BOMEditAction.Delete;
            //itemAssocParams[0].InstCount = 1;
            //itemAssocParams[0].IsIncluded = true;
            //itemAssocParams[0].IsStatic = true;
            //itemAssocParams[0].PositionNum = "position number";
            //itemAssocParams[0].UnitID = entity.Id;
            //itemAssocParams[0].UnitSize = 1;

            //connection.WebServiceManager.ItemService.UpdateItemBOMAssociations(entity.Id, itemAssocParams, BOMViewEditOptions.Defaults);
            throw new NotImplementedException();
        }

        public T GetAll(long[] ids, VDF.Vault.Currency.Connections.Connection connection)
        {
            long[] temp = ids;
			if (ids == null)
			{
				_items = _helperMethod.GetAllItems(connection);
				temp = (
                    from item 
                    in _items
					select item.Id
                    )
                    .ToArray();
			}
			List<T> itemsToRet = new List<T>();
            var relations = connection.WebServiceManager.ItemService.GetItemBOMAssociationsByItemIds(temp, true);
            foreach (var relation in relations) { 
                Console.WriteLine(relation.ParItemID);
                Console.WriteLine(relation.CldItemID);                
                Console.WriteLine("Position number : " + relation.PositionNum);
                Console.WriteLine("BOM order : " + relation.BOMOrder);
            }
            return (T)new ItemDTO((new ItemDTO.ItemResponseDTO(relations)));
        }

        public T GetById(long id, Connection connection)
        {
            Bom bom = new Bom();
            var parentChildRelationships = connection.WebServiceManager.ItemService.GetItemBOMAssociationsByItemIds(new long[] { id }, true);
            if (parentChildRelationships == null)
            {
                //throw new InterfaceException((int)InterfaceErrorCodes.BOM_OF_ITEM_NOT_EXIST, InterfaceErrorCodes.BOM_OF_ITEM_NOT_EXIST.ToString());
                return (T)new ItemDTO((new ItemDTO.ItemResponseDTO(new Bom[] { bom })));
            }
            List<ItemAssoc> parentChildRelationshipsList = parentChildRelationships.ToList();
            int index = 0;
            while (parentChildRelationshipsList.Count > 0)
            {
                var relation = parentChildRelationshipsList[index];
                if (relation.ParItemID == id)
                {
                    bom.Children.Add(new BomNode()
                    {
                        Id = relation.CldItemID,
                        Quantity = relation.Quant,
                        IsHighest = false,
                    });
                    parentChildRelationshipsList.RemoveAt(index);
                }
                else
                {
                    var cursor = bom.FindBom(bom.Children, relation.ParItemID);
                    if (cursor != null)
                    {
                        cursor.Children.Add(new BomNode()
                        {
                            Id = relation.CldItemID,
                            Quantity = relation.Quant,
                            IsHighest = false,
                        });
                        parentChildRelationshipsList.RemoveAt(index);
                    }
                }
                index++;
                if (index >= parentChildRelationshipsList.Count)
                {
                    index = 0;
                }
            }
            bom.Id = id;
            return (T)new ItemDTO((new ItemDTO.ItemResponseDTO(new Bom[] { bom })));
        }
        
        public void Update(T entity, Connection connection)
        {
            //ItemAssocParam[] itemAssocParams = new ItemAssocParam[1];
            //itemAssocParams[0] = new ItemAssocParam();
            //itemAssocParams[0].BOMOrder = 0;
            //itemAssocParams[0].CldItemID = entity.Id;
            //itemAssocParams[0].Id = 1;
            //itemAssocParams[0].Quant = 1;
            //itemAssocParams[0].EditAct = BOMEditAction.Update;
            //itemAssocParams[0].InstCount = 1;
            //itemAssocParams[0].IsIncluded = true;
            //itemAssocParams[0].IsStatic = true;
            //itemAssocParams[0].PositionNum = "position number";
            //itemAssocParams[0].UnitID = entity.Id;
            //itemAssocParams[0].UnitSize = 1;

            //connection.WebServiceManager.ItemService.UpdateItemBOMAssociations(entity.Id, itemAssocParams, BOMViewEditOptions.Defaults);
            throw new NotImplementedException();
        }
    }
}
