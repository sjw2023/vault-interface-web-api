using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Model
{
	public class PropertyDTO
	{
		public PropertyDTO(PropertyRequestDTO propertyRequestDTO, PropertyResponseDTO propertyResposeDTO) {
			Console.WriteLine("Creating PropertyDTO with copy ctor");
			_propertyRequestDTO = propertyRequestDTO;
			_propertyResponseDTO = propertyResposeDTO;
		}
		public PropertyDTO(PropertyRequestDTO propertyRequestDTO) {
			Console.WriteLine("Creating PropertyDTO with req");
			_propertyRequestDTO = new PropertyRequestDTO( propertyRequestDTO );
			_propertyRequestDTO = null;
		}
		public PropertyDTO(PropertyResponseDTO propertyResposeDTO) {
			Console.WriteLine("Creating PropertyDTO with res");
			_propertyResponseDTO = new PropertyResponseDTO (propertyResposeDTO);
			_propertyResponseDTO = null;
		}
		public PropertyDTO() { 
			Console.WriteLine("Creating PropertyDTO with default cons");
			_propertyRequestDTO = new PropertyRequestDTO();
			_propertyResponseDTO = new PropertyResponseDTO();
		}

		private PropertyRequestDTO _propertyRequestDTO;
		public PropertyRequestDTO m_PropertyRequestDTO { get { return _propertyRequestDTO; } set { _propertyRequestDTO = value; } }

		public class PropertyRequestDTO { 
			public PropertyRequestDTO(PropertyRequestDTO propertyRequestDTO) { 
				Console.WriteLine("Creating PropertyRequest with copy ctor");
				m_IdDTO = new IdDTO( propertyRequestDTO.m_IdDTO );
			}
			public PropertyRequestDTO(IdDTO idDTO) { 
				Console.WriteLine("Creating PropertyRequest dto with idDTO");
				m_IdDTO = new IdDTO(idDTO);
			}
			public PropertyRequestDTO() { 
				Console.WriteLine("Creating PropertyRequest with default cons");
				m_IdDTO = new IdDTO();
			}
			private IdDTO _idDTO;
			public IdDTO m_IdDTO { get { return _idDTO; } set { _idDTO = value; } }
				}

		private PropertyResponseDTO _propertyResponseDTO;
		public PropertyResponseDTO m_PropertyResponseDTO { get { return _propertyResponseDTO; } set { _propertyResponseDTO = value; } }
		public class PropertyResponseDTO { 

			public PropertyResponseDTO(Property[] propertyDTOs) {
				Console.WriteLine("Creating PropertyResp with items");
				m_Property = new List<Property>();
				foreach (Property property in propertyDTOs) {
					m_Property.Add(property);
				}
			}
			public PropertyResponseDTO() {
				Console.WriteLine("Creating PropertyResp with default cons");
				m_Property = new List<Property>();
			}
			public PropertyResponseDTO(PropertyResponseDTO propertyResposeDTO) {
				Console.WriteLine("Creating PropertyResp with req");
				m_Property = propertyResposeDTO.m_Property;
			}
			private List<Property> _Property;
			public List<Property> m_Property { get { return _Property; } set { _Property = value; } }	
		}
	}
}
