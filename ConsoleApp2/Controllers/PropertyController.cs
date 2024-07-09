using ConsoleApp2.Model;
using ConsoleApp2.Results;
using ConsoleApp2.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsoleApp2.Controllers
{
	public class PropertyController : ApiController, IBaseController<Property>
	{
		private readonly IBaseService<Property> _service;

		public PropertyController(IBaseService<Property> service)
		{
			_service = new LogInDecoratorService<Property>(service);
		}

		public PropertyController()
		{
			_service = new LogInDecoratorService<Property>(new LoggerDecoratorService<Property>(new PropertyService<Property>()));
		}

		[HttpPost]
		public IHttpActionResult Add([FromBody] Property entity)
		{
			_service.Add(entity, null);
			return Ok();
		}
		[HttpDelete]
		public IHttpActionResult Delete([FromBody] Property entity)
		{
			_service.Delete(entity, null);
			return Ok();
		}
		[HttpPut]
		public IHttpActionResult Update([FromBody] Property entity)
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
		public IHttpActionResult GetAll([FromBody]IdDTO idDTO)
		{
			var json = JToken.FromObject(_service.GetAll(null, null));
			return Ok(json);
		}
	}
}
