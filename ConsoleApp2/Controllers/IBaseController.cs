using ConsoleApp2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsoleApp2.Controllers
{
	public interface IBaseController<T>
	{
		//IHttpActionResult Add(T entity);
		IHttpActionResult Delete(T entity);
		IHttpActionResult Update(T entity);
		IHttpActionResult GetById(long id);
		HttpResponseMessage GetAll(T dto);
	}
}
