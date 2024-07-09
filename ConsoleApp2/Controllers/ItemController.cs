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
	public class ItemController : ApiController, IBaseController<MyItem>
	{
		//TODO : Add DI
		//TODO : Add Proxy on Decorator
		private readonly IBaseService<MyItem> _service;
		private readonly IItemService<MyItem> _itemService;
		private readonly IBaseService<Bom> _bomService;

		public ItemController(IBaseService<MyItem> service) {
			_service = new LogInDecoratorService<MyItem>(new LoggerDecoratorService<MyItem>(new ItemService<MyItem>()));
			_itemService = new LogInDecoratorService<MyItem>(new LoggerDecoratorService<MyItem>(new ItemService<MyItem>()));
			_bomService = new LogInDecoratorService<Bom>(new LoggerDecoratorService<Bom>(new BomService<Bom>()));
		}

		public ItemController()
		{
			_service = new LogInDecoratorService<MyItem>(new LoggerDecoratorService<MyItem>(new ItemService<MyItem>()));
			_itemService = new LogInDecoratorService<MyItem>(new LoggerDecoratorService<MyItem>(new ItemService<MyItem>()));
			_bomService = new LogInDecoratorService<Bom>(new LoggerDecoratorService<Bom>(new BomService<Bom>()));
		}

		[HttpPost]
		public IHttpActionResult Add([FromBody] MyItem entity)
		{
			_service.Add(entity, null);
			return Ok();
		}

		[HttpDelete]
		public IHttpActionResult Delete([FromBody] MyItem entity)
		{
			_service.Delete(entity, null);
			return Ok();
		}

		[HttpPut]
		public IHttpActionResult Update([FromBody] MyItem entity)
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
		public HttpResponseMessage GetAll([FromBody] IdDTO idDTO)
		{
			var json = JToken.FromObject(_service.GetAll( idDTO.Ids, null ));
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
		public IHttpActionResult AddBomToItem([FromBody] Bom entity, long id)
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
