using MyItem = ConsoleApp2.Model.Item;
using ConsoleApp2.Services;
using System;
using System.Text;
using System.Web.Http;
using ConsoleApp2.Model;
using ConsoleApp2.Results;
using Newtonsoft.Json.Linq;
using ConsoleApp2.Exceptions;
using System.Net.Http;
using System.Net;

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

		public ItemController(IBaseService<MyItem> service) {
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

		[HttpPost]
		public IHttpActionResult Add([FromBody] ItemDTO entity)
		{
			_service.Add(entity, null);
			return Ok();
		}

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

		[HttpGet]
		[Route("{id}")]
		public IHttpActionResult GetById(long id)
		{
			var json = JToken.FromObject(_service.GetById(id, null));
			return Ok(json);
		}
		
		[HttpGet]
		public HttpResponseMessage GetAll([FromBody] ItemDTO dto )
		{
			var json = JToken.FromObject(_service.GetAll( dto.m_ItemRequestDTO.m_IdDTO.Ids, null ));
			return new HttpResponseMessage { 
				Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
				StatusCode = HttpStatusCode.OK
			};
		}

		[HttpGet]
		[Route("bom")]
		public IHttpActionResult GetBomAll()
		{
			var json = JToken.FromObject(_bomService.GetAll(null, null));
			return Ok(json);
		}

		[HttpGet]
		[Route("bom/{id}")]
		public IHttpActionResult GetBomById(long id)
		{
			var json = JToken.FromObject(_bomService.GetById(id, null));
			return Ok(json);
		}

		[HttpPost]
		[Route("bom/{id}")]
		public IHttpActionResult AddBomToItem([FromBody] ItemDTO entity, long id)
		{
			_bomService.Add(entity, null);
			return Ok();
		}
		[HttpGet]
		[Route("name/")]
		public HttpResponseMessage GetByName([FromUri] string name)
		{
			var json = JToken.FromObject(_itemService.GetByName(name, null));
			return new HttpResponseMessage
			{
				Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
				StatusCode = HttpStatusCode.OK
			};
		}

	}
}
