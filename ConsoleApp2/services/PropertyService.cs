﻿using Autodesk.DataManagement.Client.Framework.Vault.Currency.Connections;
using ConsoleApp2.Model;
using System;
using System.Linq;
using Autodesk.Connectivity.WebServices;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Entities;
using Autodesk.DataManagement.Client.Framework.Vault.Currency.Properties;
using ConsoleApp2.Exceptions;
using DevExpress.Utils.Extensions;

namespace ConsoleApp2.Services
{
	[CustomExceptionFilter]
	public class PropertyService<T> : IPropertyService<T> where T : PropertyDTO
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

		//TODO: Deprecate this better to use GetPropertiesOfItem
		public T GetAll(long[] ids, Connection connection)
		{
			PropertyDTO toRet = new PropertyDTO();
			var conf = connection.WebServiceManager.AdminService.GetServerConfiguration();
			foreach (var entity in conf.EntClassCfgArray)
			{
				var temp = connection.PropertyManager.GetPropertyDefinitions(
				entity.Id,
				null,
				PropertyDefinitionFilter.IncludeAll);
				foreach (var property in temp)
				{
					//TODO: Generate mapper method
					Property property1 = new Property();
					property1.Id = property.Value.Id;
					if(property.Value.ValueList != null)
						property.Value.ValueList.ForEach( val => Console.WriteLine(val));
					property1.Values = property.Value.ValueList;
					property1.Name = property.Value.DisplayName;
					property.Value.AssociatedEntityClasses.ForEach(entityClass => property1.AssociatedEntityName.Add(entityClass.EntityClass.Id));
					toRet.m_PropertyResponseDTO.m_Property.Add(property1);
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
			AssocPropDefInfo[] properties = null;
			try
			{
				properties = connection.WebServiceManager.PropertyService.GetAssociationPropertyDefinitionInfosByIds(new long[] { 34 });
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			if(properties == null)
			{
				Console.WriteLine("Properties is null");
				throw new InterfaceException( (int)InterfaceErrorCodes.ITEM_NOT_EXIST, InterfaceErrorCodes.ITEM_NOT_EXIST.ToString());
				return (T)toRet;
			}
			foreach (var property in properties)
			{
				foreach (var value in property.ListValArray)
				{
					Console.WriteLine(value);
				}
			}
			return (T)toRet;
		}

		public T GetPropertiesOfItem( Connection connection )
		{
			PropertyDTO toRet = new PropertyDTO();
			PropDef[] propDefs = null;
			try
			{
				propDefs = connection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId("ITEM");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			if(propDefs == null)
			{
				Console.WriteLine("Property definitions is null");
				throw new InterfaceException( (int)InterfaceErrorCodes.ITEM_NOT_EXIST, InterfaceErrorCodes.ITEM_NOT_EXIST.ToString());
			}
			foreach( var propDef in propDefs )
			{
				if( propDef.EntClassAssocArray.Any( assoc => assoc.EntClassId.Equals(EntityClassIds.Items) ) )
				{
					//TODO: Generate mapper method
					Property property1 = new Property();
					property1.Id = propDef.Id;
					property1.Name = propDef.DispName;
					propDef.EntClassAssocArray.ForEach( assoc => property1.AssociatedEntityName.Add(assoc.EntClassId));
					toRet.m_PropertyResponseDTO.m_Property.Add(property1);
				}
			}
			return (T)toRet;
		}

		public T CheckUserPermission(Connection connection)
		{
			bool[] checkResults = null;
			try
			{
				checkResults = connection.WebServiceManager.PropertyService.CheckRolePermissions(new string[] { "AcquireFiles" },"ITEM");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
				Console.WriteLine(ex.Message);
			}
			if ( checkResults.All( result => result == true ) )
			{
				Console.WriteLine("User has permission to acquire files");
				return (T)Activator.CreateInstance(typeof(T));
			}
			else
			{
				Console.WriteLine("User does not have permission to acquire files");
				throw new InterfaceException( (int)InterfaceErrorCodes.PERMISSION_DENIED, InterfaceErrorCodes.PERMISSION_DENIED.ToString());
			}
		}
	}
}
