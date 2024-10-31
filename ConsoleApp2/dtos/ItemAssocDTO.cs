using Autodesk.Connectivity.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.dtos
{
    public class ItemAssocDTO
	{
		public long ChildId { get; set; }
		public long ParentId { get; set; }
		public double Quantity { get; set; }
		public int BOMOrder { get; set; }

		public ItemAssocDTO() { }
		public ItemAssocDTO(ItemAssoc itemAssoc) { 
			ChildId = itemAssoc.CldItemID;
			ParentId = itemAssoc.ParItemID;
			Quantity = itemAssoc.Quant;
			BOMOrder = itemAssoc.BOMOrder;	
		}
		public ItemAssocDTO(long childId, long parentId, double quantity, int bomOrder)
		{
			ChildId = childId;
			ParentId = parentId;
			Quantity = quantity;
			BOMOrder = bomOrder;
		}
		public ItemAssocDTO(ItemAssocDTO itemAssocDTO)
		{
			ChildId = itemAssocDTO.ChildId;
			ParentId = itemAssocDTO.ParentId;
			Quantity = itemAssocDTO.Quantity;
			BOMOrder = itemAssocDTO.BOMOrder;
		}
	}
}
