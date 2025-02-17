using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using DevExpress.Utils.Extensions;

namespace ConsoleApp2.Services
{
	[CustomExceptionFilter]
	public class PropertyService<T> : IBaseService<T> where T : PropertyDTO
	{
		public void Add(T entity, Connection connection)
		{
			throw new NotImplementedException();
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
			//Guid guid = Guid.NewGuid();
			//PropDefInfo propDefInfo = connection.WebServiceManager.PropertyService.AddPropertyDefinition(
			//guid.ToString(),
			//entity.m_PropertyResposeDTO.m_Property.Name, 
			//DataType.String,
			//true, 
			//true, 
			//string.Empty,
			////entity.m_PropertyResposeDTO.m_Property.AssociatedEntityName.ToArray(),
			//null, 
			//null, 
			//null
			//);
		}

		public void Delete(T entity, Connection connection)
		{
			throw new NotImplementedException();
			//connection.WebServiceManager.PropertyService.DeletePropertyDefinitions(new long[] { entity.m_PropertyResponseDTO.m_Property[0].Id });
		}

		public void Update(T entity, Connection connection)
		{
			throw new NotImplementedException();
			//Dictionary<string, BhvCfg[]> bhvCfgMap = new Dictionary<string, BhvCfg[]>();
			//var servConf = connection.WebServiceManager.AdminService.GetServerConfiguration();
			//servConf.EntClassCfgArray.ForEach(entClass =>
			//{
			//	var bhvCfg = connection.WebServiceManager.BehaviorService.GetBehaviorConfigurationsByNames(entClass.Id, new string[] { "UserDefinedProperty" });
			//	bhvCfgMap.Add(entClass.Id, bhvCfg);
			//});
			//var propDef = bhvCfgMap.Select((k, v) => v == entity.m_PropertyResposeDTO.m_Property.Id);
			//List<EntClassAssoc> entClassAssocList = new List<EntClassAssoc>();
			//entity.m_PropertyResposeDTO.m_Property.AssociatedEntityName.ForEach(name => { 
			//	EntClassAssoc entClassAssoc = new EntClassAssoc(); 
			//	entClassAssoc.EntClassId = name;
			//	entClassAssoc.MapDirection= AllowedMappingDirection.Write;
			//	entClassAssocList.Add(entClassAssoc); 
			//		});
			//PropDef prop = new PropDef();
			//prop.Id = entity.m_PropertyResposeDTO.m_Property.Id;
			//prop.DispName = entity.m_PropertyResposeDTO.m_Property.Name;
			//prop.DfltVal = "DefaultListValue1";
			//prop.EntClassAssocArray = entClassAssocList.ToArray();
			//prop.IsAct = true;
			//prop.IsBasicSrch = true;
			//prop.IsSys = false;
			//prop.Typ = DataType.String;
			//connection.WebServiceManager.PropertyService.UpdatePropertyDefinitionInfo(
			//	prop,
			//	null,
			//	null,
			//	new string[] { "DefaultListValue1" }
			//	);
		}

		public T GetAll(long[] ids, Connection connection)
		{
			PropertyDTO toRet = new PropertyDTO();
			var conf = connection.WebServiceManager.AdminService.GetServerConfiguration();

			foreach (var entity in conf.EntClassCfgArray)
			{
				// var temp = connection.PropertyManager.GetPropertyDefinitions(
				// entity.Id,
				// null,
				// PropertyDefinitionFilter.IncludeAll);
				var temp2 = connection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId(
					entity.Id);
				// foreach (var property in temp)
				// {
				// 	Property property1 = new Property();
				// 	property1.Id = property.Value.Id;
				// 	property1.Name = property.Value.DisplayName;
				// 	property.Value.AssociatedEntityClasses.ForEach(entityClass => property1.AssociatedEntityName.Add(entityClass.EntityClass.Id));
				// 	toRet.m_PropertyResponseDTO.m_Property.Add(property1);
				// }

				foreach (var propDef in temp2)
				{
					Property property1 = new Property();
					property1.Id = propDef.Id;
					property1.Name = propDef.DispName;
					var iterator = propDef.DfltVal.YieldArray();
					iterator.ForEach(val =>
						Console.WriteLine(object.ReferenceEquals(val, null) ? "null" : val.ToString()));
				}
			}

			return (T)toRet;
		}

		public T GetById(long id, Connection connection)
		{
			var entity = connection.PropertyManager.GetPropertyDefinitionById(id);
			T result = (T)Activator.CreateInstance(typeof(T));
			Property entity1 = new Property
			{
				Id = entity.Id,
				Name = entity.DisplayName
			};
			entity.AssociatedEntityClasses.ForEach(classes => entity1.AssociatedEntityName.Add(classes.EntityClass.Id));
			result.m_PropertyResponseDTO.m_Property.Add(entity1);
			return result;
		}

		public T GetPropertyValues( Connection connection )
		{
			PropertyDTO toRet = new PropertyDTO();
			var conf = connection.WebServiceManager.AdminService.GetServerConfiguration();
			// connection.WebServiceManager.PropertyService.GetPropertiesByEntityIds("ITEM", new long[] {});
			var properties = connection.WebServiceManager.PropertyService.GetAssociationPropertyDefinitionInfosByIds(new 
				long[] 
			{
				41
				
			});
			foreach (var property in properties)
			{
				foreach (var value in property.ListValArray)
				{
					Console.WriteLine(value);
				}
				
			}
			return (T)toRet;
		}
	}
}
