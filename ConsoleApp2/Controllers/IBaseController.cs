using System.Net.Http;
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