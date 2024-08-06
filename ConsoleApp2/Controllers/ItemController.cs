using MyItem = ConsoleApp2.Model.Item;
using ConsoleApp2.Services;
using System.Web.Http;
using ConsoleApp2.Model;
using ConsoleApp2.Results;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using Autodesk.Connectivity.WebServices;

namespace ConsoleApp2.Controllers
{
    [RoutePrefix("api/item")]
    public class ItemController : ApiController, IBaseController<ItemDTO>
    {
        //TODO : Add DI
        //TODO : Add Proxy on Decorator
        private readonly IBaseService<ItemDTO> _service;
        private readonly IItemService<ItemDTO> _itemService;
        private readonly IBaseService<ItemDTO> _bomService;

        public ItemController(IBaseService<MyItem> service)
        {
            _service = new LogInDecoratorService<ItemDTO>(new LoggerDecoratorService<ItemDTO>(new ItemService<ItemDTO>()));
            _itemService = new LogInDecoratorService<ItemDTO>(new LoggerDecoratorService<ItemDTO>(new ItemService<ItemDTO>()));
            _bomService = new LogInDecoratorService<ItemDTO>(new LoggerDecoratorService<ItemDTO>(new BomService<ItemDTO>()));
        }

        public ItemController()
        {
            _service = new LogInDecoratorService<ItemDTO>(new LoggerDecoratorService<ItemDTO>(new ItemService<ItemDTO>()));
            _itemService = new LogInDecoratorService<ItemDTO>(new LoggerDecoratorService<ItemDTO>(new ItemService<ItemDTO>()));
            _bomService = new LogInDecoratorService<ItemDTO>(new LoggerDecoratorService<ItemDTO>(new BomService<ItemDTO>()));
        }

        //[HttpPost]
        //public IHttpActionResult Add([FromBody] ItemDTO entity)
        //{
        //	_service.Add(entity, null);
        //	return Ok();
        //}

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] ItemDTO entity)
        {
            _service.Delete(entity, null);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] ItemDTO entity)
        {
            _service.Update(entity, null);
            return Ok();
        }

        [HttpPost]
        [Route("{id}")]
        public IHttpActionResult GetById(long id)
        {
            var json = JToken.FromObject(_service.GetById(id, null));
            return Ok(json);
        }

        [HttpPost]
        public HttpResponseMessage GetAll([FromBody] ItemDTO dto)
        {
            var json = JToken.FromObject(_service.GetAll(dto.m_ItemRequestDTO.m_IdDTO.Ids, null));
            return new HttpResponseMessage
            {
                Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpPost]
        [Route("bom")]
        public IHttpActionResult GetBomAll()
        {
            var json = JToken.FromObject(_bomService.GetAll(null, null));
            return Ok(json);
        }

        [HttpPost]
        [Route("bom/{id}")]
        public IHttpActionResult GetBomById(long id)
        {
            var json = JToken.FromObject(_bomService.GetById(id, null));
            return Ok(json);
        }

        //[HttpPost]
        //[Route("bom/{id}")]
        //public IHttpActionResult AddBomToItem([FromBody] ItemDTO entity, long id)
        //{
        //	_bomService.Add(entity, null);
        //	return Ok();
        //}
        [HttpPost]
        [Route("name/{name}")]
        public HttpResponseMessage GetByName(string name)
        {
            var json = JToken.FromObject(_itemService.GetByName(name, null));
            return new HttpResponseMessage
            {
                Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpGet]
        [Route("test")]
        public IHttpActionResult GetBySrchCond([FromUri] ItemSearchRequestDTO itemSearchRequestDTO)
        {
            string bookmark = null;
            SrchStatus searchstatus = null;
            //?LatestReleasedOnly=true&SearchCond=Name%3D%27%25%27&SearchSort=Name%20ASC
            var json = JToken.FromObject(_itemService.GetBySchCond(itemSearchRequestDTO.SrchCond, itemSearchRequestDTO.SrchSort, itemSearchRequestDTO.LatestReleasedOnly, ref bookmark, out searchstatus, null));
            return Ok(json);
        }

        [HttpPost]
        [Route("createdOrModified")]
        public IHttpActionResult GetByDate([FromBody] ItemDTO itemDto)
        {
            var json = JToken.FromObject(_itemService.GetByDate(itemDto.m_ItemRequestDTO.Date, null));
            return Ok(json);
        }
    }
}
