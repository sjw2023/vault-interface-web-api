using System.Web.Http;
using ConsoleApp2.dtos;

namespace ConsoleApp2.Controllers
{
    public interface IFileController: IBaseController< IFileRequestDto >
    {
        
    }
}