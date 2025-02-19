using ConsoleApp2.Model;
using ConsoleApp2.Util;

namespace ConsoleApp2.Services
{
    [CustomExceptionFilter]
    public class OchestrationService
    {
        //TODO : Implement ochestration logic
        private readonly ItemService<ItemDTO> _itemService = new ItemService<ItemDTO>();
        private readonly PropertyService<PropertyDTO> _propertyService = new PropertyService<PropertyDTO>();

    }
}