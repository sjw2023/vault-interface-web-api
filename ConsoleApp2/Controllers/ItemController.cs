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

namespace ConsoleApp2.Controllers
{
	public class ItemController : ApiController, IBaseController<MyItem>
	{
		private readonly IBaseService<MyItem> _service;

		public ItemController(IBaseService<MyItem> service) {
			_service = service;
		}

		public ItemController()
		{
			_service = new ItemService<MyItem>();
		}

		[HttpPost]
		public IHttpActionResult Add([FromBody] MyItem entity)
		{
			Console.WriteLine(entity.Id);
			_service.Add(entity);
			return new ItemResult(entity, Request);
		}

		[HttpDelete]
		public IHttpActionResult Delete([FromBody] MyItem entity)
		{
			_service.Delete(entity);
			return new ItemResult(entity, Request);
		}

		[HttpPut]
		public IHttpActionResult Update([FromBody] MyItem entity)
		{
			_service.Update(entity);
			return new ItemResult(entity, Request);
		}

		[HttpGet]
		public IHttpActionResult GetById(long id)
		{
			return new ItemResult(_service.GetById(id), Request);
		}

		[HttpGet]
		public IHttpActionResult GetAll()
		{
			_service.GetAll();
			//return new ItemsResult( _service.GetAll().ToArray(), Request);
			return new ItemResult(new MyItem(), Request);
		}

	}
}
