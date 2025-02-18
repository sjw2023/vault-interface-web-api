using System.Web.Http;

namespace ConsoleApp2.Controllers
{
    public interface class IFileController: ApiController, IBaseController<T> where T: FileDto
    {
        
    }
}