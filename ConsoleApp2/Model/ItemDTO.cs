using Autodesk.Connectivity.WebServices;
using DevExpress.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class ItemDTO
	{

		private ItemRequestDTO _itemRequestDTO;
		public ItemRequestDTO m_ItemRequestDTO { get { return _itemRequestDTO; } set { _itemRequestDTO = value; } }
		private ItemResponseDTO _itemResponseDTO;
		public ItemResponseDTO m_ItemResponseDTO { get { return _itemResponseDTO; } set { _itemResponseDTO = value; } }

		public ItemDTO(ItemResponseDTO itemResponseDTO) { 
			Console.WriteLine("Creating ItemDTo with resps");
			_itemResponseDTO = new ItemResponseDTO(itemResponseDTO.ItemDTOs, itemResponseDTO.BomDTOs);
			Console.WriteLine("Returning from ItemDTO with resps");
		}
		public ItemDTO(ItemRequestDTO itemRequestDTO) { 
			Console.WriteLine("Creating ItemDTO with req");
			_itemRequestDTO = new ItemRequestDTO(itemRequestDTO.m_IdDTO);
			Console.WriteLine("Returning from ItemDTo with req");
		}
		public ItemDTO(ItemRequestDTO itemRequestDTO, ItemResponseDTO itemResponseDTO) { 
			Console.WriteLine("Creating ItemDto with res, req");
			_itemRequestDTO = new ItemRequestDTO(itemRequestDTO.m_IdDTO);
			_itemResponseDTO = new ItemResponseDTO(itemResponseDTO.BomDTOs);
		}
		public ItemDTO(ItemDTO itemDTO) { 
			Console.WriteLine("Creating ItemDto with Copy ctor");
			_itemRequestDTO = itemDTO.m_ItemRequestDTO;
			Console.WriteLine("Returning from ItemDto Copy ctor");
			//_itemResponseDTO = itemDTO.m_ItemResponseDTO;
		}
		private ItemDTO()
		{
			Console.WriteLine("Creating ItemDTO with default cons");
			_itemRequestDTO = new ItemRequestDTO(new ItemRequestDTO(new IdDTO()));
			Console.WriteLine("Returning from ItemDto default ctor");
		}
		public class ItemRequestDTO { 
			public ItemRequestDTO(ItemRequestDTO itemRequestDTO) { 
				Console.WriteLine("Creating ItemRequest with copy ctor");
				//m_IdDTO = new IdDTO( itemRequestDTO.m_IdDTO );
				m_IdDTO = itemRequestDTO.m_IdDTO;
				Console.WriteLine("Returning from ItemRequestDTO copy ctor");
			}
			public ItemRequestDTO(IdDTO idDTO) { 
				Console.WriteLine("Creating ItemRequest dto with idDTO");
				//m_IdDTO = new IdDTO(idDTO);
				m_IdDTO = idDTO;
				Console.WriteLine("Returning ItemRequest dto with idDTO");
			}
			private IdDTO m_idDTO;
			public IdDTO m_IdDTO { get { return m_idDTO; } set { m_idDTO = value; } }
		}

		public class ItemResponseDTO {
			public ItemResponseDTO(Item[] itemDTOs, Bom[] boms) {
				Console.WriteLine("Creating ItemResp with items, boms");
				m_itemDTOs = itemDTOs;
				m_bomDTOs = boms;
			}
			public ItemResponseDTO(Item[] itemDTOs) {
				Console.WriteLine("Creating ItemResp with items");
				m_itemDTOs = itemDTOs;
			}
			public ItemResponseDTO(Bom[] bomDTOs) { 
				Console.WriteLine("Creating ItemResp with boms");
				m_bomDTOs = bomDTOs;
			}
			
			private Item[] m_itemDTOs;
			public Item[] ItemDTOs { get { return m_itemDTOs; } set { m_itemDTOs = value; } }
			private Bom[] m_bomDTOs;
			public Bom[] BomDTOs { get { return m_bomDTOs; } set { m_bomDTOs = value; } }
		}
		
	}
}
