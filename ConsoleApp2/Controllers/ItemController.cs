using Autodesk.Connectivity.WebServices;
using ConsoleApp2.Controllers;
using MyItem = ConsoleApp2.Model.Item;
using ConsoleApp2.Services;
using DevExpress.Pdf.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ConsoleApp2.Model;
using ConsoleApp2.Results;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2.Controllers
{
	public class ItemController : ApiController, IBaseController<MyItem>
	{
		//TODO : Add DI
		//TODO : Add Proxy on Decorator
		private readonly IBaseService<MyItem> _service;

		public ItemController(IBaseService<MyItem> service) {
			_service = new LogInDecoratorService<MyItem>(new LoggerDecoratorService<MyItem>(new ItemService<MyItem>()));
		}

		public ItemController()
		{
			_service = new LogInDecoratorService<MyItem>(new LoggerDecoratorService<MyItem>(new ItemService<MyItem>()));
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
		public IHttpActionResult GetById(long id)
		{
			var json = JToken.FromObject(_service.GetById(id, null));
			return Ok(json);
		}

		[HttpGet]
		public IHttpActionResult GetAll()
		{
			var json = JToken.FromObject(_service.GetAll(null));
			return Ok(json);
		}

	}
}
