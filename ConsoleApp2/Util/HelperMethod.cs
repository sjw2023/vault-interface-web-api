using Autodesk.Connectivity.WebServices;
using ConsoleApp2.Model;
using System.Collections.Generic;
using System.Linq;
using VaultItem = Autodesk.Connectivity.WebServices.Item;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Util
{
	public class HelperMethod
	{
		/// <summary>
		/// Helper method
		/// TODO : Refactor this method to utilize the search conditions and etc
		/// </summary>
		/// <param name="connection"></param>
		/// <returns></returns>
		public List<VaultItem> GetAllItems(VDF.Vault.Currency.Connections.Connection connection)
		{
			List<VaultItem> items = new List<VaultItem>();
			string bookmark = null;
			SrchStatus status = null;
			while (status == null || status.TotalHits > items.Count)
			{
				items.AddRange(connection.WebServiceManager.ItemService.FindItemRevisionsBySearchConditions(null, null, true, ref bookmark, out status));
			}
			return items;
		}
		
		/// <summary>
		/// helper method
		/// </summary>
		/// <param name="connection"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public List<PropInstDTO> GetPropInst(VDF.Vault.Currency.Connections.Connection connection, long[] ids, string entityClassId)
		{
			//Get property definition infos by entity class id
			var propDefInfos = connection.WebServiceManager.PropertyService.GetPropertyDefinitionInfosByEntityClassId(entityClassId, null);
			//Extract property definition ids
			var propDefIds = from info in propDefInfos
							 select info.PropDef.Id;
			//Get latest items by item master ids
			var latestItems = connection.WebServiceManager.ItemService.GetLatestItemsByItemMasterIds(ids);
			//Extract item ids
			var idsParameter = from item in latestItems
					  select item.Id;
			//Get properties by item ids
			var properties = connection.WebServiceManager.PropertyService.GetProperties(entityClassId, idsParameter.ToArray(), propDefIds.ToArray());
			List<PropInstDTO> ret = new List<PropInstDTO>();
			foreach (var _ in properties)
			{
				PropInstDTO instance = new PropInstDTO();
				instance.Id = _.EntityId;
				instance.PropDefId = _.PropDefId;
				var propDef = from propDefInfo in propDefInfos
							  where propDefInfo.PropDef.Id == _.PropDefId
							  select propDefInfo;
				instance.Name = propDef.First().PropDef.SysName;
				if (_.Val == null)
				{
					instance.Value = "null";
				}
				else
				{
					if (instance.Name == "Thumbnail")
					{
						instance.Value = _.Val;
					}
					else { 
						instance.Value = _.Val.ToString();
					}
				}
				ret.Add(instance);
			}
			//var itemResult = connection.WebServiceManager.ItemService.GetItemsByRevisionIds(new long[] { 40573 }, true);
			//var itemTest = connection.WebServiceManager.ItemService.GetLatestItemByItemMasterId(39920);
			return ret;
		}
	}
}
