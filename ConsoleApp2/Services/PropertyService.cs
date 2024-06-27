using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;
using Autodesk.Connectivity.WebServices;
using Autodesk.Connectivity.WebServicesTools;
using Autodesk.DataManagement.Client.Framework.Internal.ExtensionMethods;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Entities;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties;
using ConsoleApp2.Model;
using DevExpress.Pdf.Native.BouncyCastle.Utilities;
using DevExpress.XtraEditors;
using VaultItem = Autodesk.Connectivity.WebServices.Item;
using VDF = Autodesk.DataManagement.Client.Framework;

namespace ConsoleApp2.Services
{
	public class PropertyService<T> : IBaseService<T> where T : Property
	{
		public void Add(T entity, Connection connection)
		{
			//파일 관련 프로퍼티 읽기
			//PropDef[] props = connection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("FILE");

			//인벤터 파일 연결 모니커 생성
			//string Moniker = "";
			//foreach (PropDef prop in props)
			//{
			//	if (prop.DispName == "Test")
			//	{
			//		PropDefInfo[] propInfos = connection.WebServiceManager.
			//			PropertyService.GetPropertyDefinitionInfosByEntityClassId(
			//			"FILE", new long[] { prop.Id });
			//		foreach (PropDefInfo propInfo in propInfos)
			//		{
			//			Moniker = propInfo.EntClassCtntSrcPropCfgArray[0].
			//				CtntSrcPropDefArray[0].Moniker.Split('!')[1];
			//		}
			//	}
			//}

			//인벤터 파일 연결 속성 추가
			//CtntSrcPropDef ctntSrcPropDef = new CtntSrcPropDef();
			//ctntSrcPropDef.DispName = "UserPropertyTest";
			//ctntSrcPropDef.CanCreateNew = true;
			//ctntSrcPropDef.Classification = Classification.Custom;
			//ctntSrcPropDef.CtntSrcDefTyp = CSPDefTypes.File;
			//ctntSrcPropDef.CtntSrcId = 4;
			//ctntSrcPropDef.MapDirection = AllowedMappingDirection.Write;
			//ctntSrcPropDef.Typ = DataType.String;
			//ctntSrcPropDef.Moniker = "4!" + Moniker + "!nvarchar";

			//EntClassCtntSrcPropCfg entClsCtntSrcPropCfg = new EntClassCtntSrcPropCfg();
			//entClsCtntSrcPropCfg.EntClassId = "FILE";
			//entClsCtntSrcPropCfg.CanCreateNewArray = new bool[] { true };
			//entClsCtntSrcPropCfg.CtntSrcPropDefArray = new CtntSrcPropDef[] { ctntSrcPropDef };
			//entClsCtntSrcPropCfg.MapDirectionArray = new Autodesk.Connectivity.WebServices.MappingDirection[] { Autodesk.Connectivity.WebServices.MappingDirection.Write };
			//entClsCtntSrcPropCfg.MapTypArray = new Autodesk.Connectivity.WebServices.MappingType[] { Autodesk.Connectivity.WebServices.MappingType.Constant };
			//entClsCtntSrcPropCfg.PriorityArray = new int[] { 0 };

			//프로퍼티 추가
			try
			{
				Guid guid = Guid.NewGuid();
				PropDefInfo propDefInfo = connection.WebServiceManager.PropertyService.AddPropertyDefinition(
					guid.ToString(), 
					entity.Name, 
					DataType.String,
					true, 
					true, 
					string.Empty, 
					entity.AssociatedEntityName,
					null, 
					null, 
					null
					);
			} catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void Delete(T entity, Connection connection)
		{
			try {
				connection.WebServiceManager.PropertyService.DeletePropertyDefinitions(new long[] { entity.Id });
			}catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public void Update(T entity, Connection connection)
		{
			try {
				var ret = connection.WebServiceManager.BehaviorService.GetBehaviorConfigurationsByNames("ITEM", new string[] { "UserDefinedProperty" });
				
				var id = ret.Select(bhv => bhv.BhvArray.Select(elem => elem.Id == entity.Id));
	
	            //TODO :  Test the code
				List<EntClassAssoc> entClassAssocList = new List<EntClassAssoc>();
				entity.AssociatedEntityName.ForEach(name => { 
					EntClassAssoc entClassAssoc = new EntClassAssoc(); 
					entClassAssoc.EntClassId = name;
					entClassAssoc.MapDirection= AllowedMappingDirection.Write;
					entClassAssocList.Add(entClassAssoc); 
						});

				PropDef prop = new PropDef();
				prop.Id = entity.Id;
				prop.DispName = entity.Name;
				prop.DfltVal = "DefaultListValue1";
				prop.EntClassAssocArray = new EntClassAssoc[] { entClassAssoc };
				prop.IsAct = true;
				prop.IsBasicSrch = true;
				prop.IsSys = false;
				prop.Typ = DataType.String;

				connection.WebServiceManager.PropertyService.UpdatePropertyDefinitionInfo(
					prop,
					null,
					null,
					new string[] { "DefaultListValue1" }
					);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public IEnumerable<T> GetAll(Connection connection)
		{
			List<T> entities = new List<T>();

			var conf = connection.WebServiceManager.AdminService.GetServerConfiguration();
			foreach (var entity in conf.EntClassCfgArray) { 
				var temp = connection.PropertyManager.GetPropertyDefinitions(
				entity.Id,
				null,
				PropertyDefinitionFilter.IncludeAll);
                foreach (var property in temp)
                {
					T elem = (T)Activator.CreateInstance(typeof(T));
					elem.Id = property.Value.Id;
					// TODO : Test it
					elem.AssociatedEntityName = property.Value.AssociatedEntityName;
					elem.Name = property.Value.DisplayName;
					entities.Add(elem);
                }
            }
			return entities;
		}

		public T GetById(long id, Connection connection)
		{
			try
			{
				var entity = connection.PropertyManager.GetPropertyDefinitionById(id);
				T result = (T)Activator.CreateInstance(typeof(T));
				result.Id = entity.Id;
				result.Name = entity.DisplayName;
				entity.AssociatedEntityName.ForEach(name => result.EntityName.Add(name));
				return result;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}

		}
	}
}
