﻿using Autodesk.Connectivity.WebServices;
using DevExpress.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp2.Model
{
	public class ItemDTO
	{

		public ItemRequestDTO m_ItemRequestDTO { get; set; }
		public ItemResponseDTO m_ItemResponseDTO { get; set; }
		public ItemDTO(ItemDTO itemDTO) { 
			Console.WriteLine("Creating ItemDto with Copy ctor");
			m_ItemRequestDTO = itemDTO.m_ItemRequestDTO;
			Console.WriteLine("Returning from ItemDto Copy ctor");
			//_itemResponseDTO = itemDTO.m_ItemResponseDTO;
		}
		public ItemDTO(ItemResponseDTO itemResponseDTO) { 
			Console.WriteLine("Creating ItemDTo with resps");
			m_ItemResponseDTO = new ItemResponseDTO(itemResponseDTO.ItemDTOs, itemResponseDTO.BomDTOs);
			Console.WriteLine("Returning from ItemDTO with resps");
		}
		public ItemDTO(ItemRequestDTO itemRequestDTO) { 
			Console.WriteLine("Creating ItemDTO with req");
			m_ItemRequestDTO = new ItemRequestDTO(itemRequestDTO.m_IdDTO, itemRequestDTO.Date);
			Console.WriteLine("Returning from ItemDTo with req");
		}
		public ItemDTO(ItemRequestDTO mItemRequestDto = null, ItemResponseDTO mItemResponseDto = null)
		{
			m_ItemRequestDTO = mItemRequestDto ?? throw new ArgumentNullException(nameof(mItemRequestDto));
			m_ItemResponseDTO = mItemResponseDto ?? throw new ArgumentNullException(nameof(mItemResponseDto));
		}
		
		private ItemDTO()
		{
			Console.WriteLine("Creating ItemDTO with default cons");
			m_ItemRequestDTO = new ItemRequestDTO(new ItemRequestDTO(new IdDTO()));
			Console.WriteLine("Returning from ItemDto default ctor");
		}
		public class ItemRequestDTO {
			public ItemRequestDTO(ItemRequestDTO itemRequestDTO) { 
				Console.WriteLine("Creating ItemRequest with copy ctor");
				//m_IdDTO = new IdDTO( itemRequestDTO.m_IdDTO );
				m_IdDTO = itemRequestDTO.m_IdDTO;
				Date = itemRequestDTO.Date;
				Console.WriteLine("Returning from ItemRequestDTO copy ctor");
			}
			public ItemRequestDTO(IdDTO idDTO)
			{
				Console.WriteLine("Creating ItemRequest dto with idDTO");
				//m_IdDTO = new IdDTO(idDTO);
				m_IdDTO = idDTO;
				Console.WriteLine("Returning ItemRequest dto with idDTO");
			}
			public ItemRequestDTO(string date)
			{
				Console.WriteLine("Creating ItemRequest dto with date");
				//m_IdDTO = new IdDTO(idDTO);
				Date = date;
				Console.WriteLine("Returning ItemRequest dto with date");
			}

			public ItemRequestDTO(IdDTO mIdDto = null, string date = null)
			{
				m_idDTO = mIdDto ?? throw new ArgumentNullException(nameof(mIdDto));
				Date = date ?? throw new ArgumentNullException(nameof(date));
			}

			public ItemRequestDTO()
			{
			}

			private IdDTO m_idDTO { get; set; }
			[JsonPropertyName("m_IdDTO")]
			public IdDTO m_IdDTO { get { return m_idDTO; } set { m_idDTO = value; } }
			//TODO : Add validation of data
			[JsonPropertyName("Date")]
			public string Date { get; set; }
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
