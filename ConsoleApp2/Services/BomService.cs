using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Internal.ExtensionMethods;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Model;
using ConsoleApp2.Util;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars.Docking2010.DragEngine;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using static DevExpress.XtraPrinting.Native.ExportOptionsPropertiesNames;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public class BomService<T> : IBaseService<T> where T : Bom, new()
	{
		private HelperMethod _helperMethod = new HelperMethod();
		private IEnumerable<Autodesk.Connectivity.WebServices.Item> _items;

		public void Add(T entity, Connection connection)
		{
			throw new NotImplementedException();
			try {
				Autodesk.Connectivity.WebServices.Item newest = connection.WebServiceManager.ItemService.GetLatestItemByItemMasterId(entity.Id);
				var revs = connection.WebServiceManager.ItemService.EditItems( new long[] { newest.RevId });
				var bom2 = connection.WebServiceManager.ItemService.GetItemBOMByItemIdAndDate(31581, DateTime.MinValue, BOMTyp.Latest, BOMViewEditOptions.ReturnBOMFragmentsOnEdits);

				var itemassoc = bom2.ItemAssocArray.FirstOrDefault(x => x.CldItemMasterID == 31581);
				ItemAssocParam[] itemAssocParams = new ItemAssocParam[1];
				itemAssocParams[0] = new ItemAssocParam();
				//itemAssocParams[0].BOMOrder = newest;
				//itemAssocParams[0].CldItemID = entity.ChildId;
				itemAssocParams[0].Id = itemassoc.Id;
				//itemAssocParams[0].Quant = 1;
				itemAssocParams[0].EditAct = BOMEditAction.Update;
				//itemAssocParams[0].InstCount = 1;
				//itemAssocParams[0].IsIncluded = true;
				//itemAssocParams[0].IsStatic = true;
				//itemAssocParams[0].PositionNum = "position number";
				//itemAssocParams[0].UnitID = newest.UnitId;
				//itemAssocParams[0].UnitSize = 1;

				

				connection.WebServiceManager.ItemService.UpdateItemBOMAssociations( entity.Id, itemAssocParams, BOMViewEditOptions.ReturnBOMFragmentsOnEdits);
				connection.WebServiceManager.ItemService.UpdateAndCommitItems(revs);
			}
			catch (Exception e)
			{
				connection.WebServiceManager.ItemService.UndoEditItems(new long[] {  });
				Console.WriteLine(e.Message);
				//Console.WriteLine(e.StackTrace);
				//Console.WriteLine(e.InnerException);
				//Console.WriteLine(e.Source);
				//Console.WriteLine(e.TargetSite);
				//Console.WriteLine(e.Data);
				//Console.WriteLine(e.HelpLink);
				//Console.WriteLine(e.HResult);
			}
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

		public IEnumerable<T> GetAll(long [] ids, VDF.Vault.Currency.Connections.Connection connection)
		{
			////TODO : Updating _items is not done with this code. Refactor to check items update status.
			//if (_items == null)
			//{
			//	_items = _helperMethod.GetAllItems(connection);
			//}
			//List<T> itemsToRet = new List<T>();
			//var ids = from item in _items
			//		  select item.Id;
			//var relations = connection.WebServiceManager.ItemService.GetItemBOMAssociationsByItemIds(ids.ToArray(), true);
			//foreach (var id in ids) 
			//{ 
			//	var children = from relation in relations
			//				   where relation.ParItemID == id
			//				   select relation.CldItemID;
			//	T bomEle= (T)Activator.CreateInstance(typeof(T));
			//	itemsToRet.Add(bomEle);
			//}
			//return itemsToRet;
			throw new NotImplementedException();
		}

		public T GetById(long id, Connection connection)
		{
			try
			{
				T bom = (T)Activator.CreateInstance(typeof(T));
				var parentChildRelationships = connection.WebServiceManager.ItemService.GetItemBOMAssociationsByItemIds(new long[] { id }, true);
				List<ItemAssoc> parentChildRelationshipsList = parentChildRelationships.ToList();
				int index = 0;
				while (parentChildRelationshipsList.Count>0) {
					var relation = parentChildRelationshipsList[index];
					if (relation.ParItemID == id)
					{
						bom.Children.Add(new BomNode() { Id = relation.CldItemID });
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
				return bom;
			}
			catch (Exception e) { 
				var ex = (VaultServiceErrorException)e;
				Console.WriteLine($"{ex.Message} | {ex.Detail.InnerText}");
				throw new Exception(ex.Message);
			}
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
