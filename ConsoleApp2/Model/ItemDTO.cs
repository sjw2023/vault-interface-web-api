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
		[JsonIgnore]
		public ItemRequestDTO m_ItemRequestDTO { get { return _itemRequestDTO; } set { _itemRequestDTO = value; } }
		private ItemResponseDTO _itemResponseDTO;
		[JsonIgnore]
		public ItemResponseDTO m_ItemResponseDTO { get { return _itemResponseDTO; } set { _itemResponseDTO = value; } }

		public ItemDTO(ItemResponseDTO itemResponseDTO) { 
			Console.WriteLine("Creating ItemDTo with resps");
			_itemResponseDTO = new ItemResponseDTO(itemResponseDTO.ItemDTOs, itemResponseDTO.BomDTOs);
		}
		public ItemDTO(ItemRequestDTO itemRequestDTO) { 
			Console.WriteLine("Creating ItemDTO with req");
			_itemRequestDTO = new ItemRequestDTO(itemRequestDTO.m_IdDTO);
		}
		public ItemDTO(ItemRequestDTO itemRequestDTO, ItemResponseDTO itemResponseDTO) { 
			Console.WriteLine("Creating ItemDto with res, req");
			_itemRequestDTO = new ItemRequestDTO(itemRequestDTO.m_IdDTO);
			_itemResponseDTO = new ItemResponseDTO(itemResponseDTO.BomDTOs);
		}
		public ItemDTO(ItemDTO itemDTO) { 
			_itemRequestDTO = itemDTO.m_ItemRequestDTO;
			Console.WriteLine("Creating ItemDto with Copy ctor");
			//_itemResponseDTO = itemDTO.m_ItemResponseDTO;
		}
		private ItemDTO()
		{
			Console.WriteLine("Creating with default cons");
			_itemRequestDTO = new ItemRequestDTO(new ItemRequestDTO(new IdDTO()));
		}
		public class ItemRequestDTO { 
			public ItemRequestDTO(ItemRequestDTO itemRequestDTO) { 
				Console.WriteLine("Creating ItemRequest with copy cons");
				m_IdDTO = new IdDTO( itemRequestDTO.m_IdDTO );
			}
			public ItemRequestDTO(IdDTO idDTO) { 
				Console.WriteLine("Creating ItemRequest dto with idDTO");
				m_IdDTO = new IdDTO(idDTO);
			}
			private IdDTO _idDTO;
			public IdDTO m_IdDTO { get { return _idDTO; } set { _idDTO = value; } }
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
