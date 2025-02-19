using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConsoleApp2.dtos;
using ConsoleApp2.Results;
using ConsoleApp2.Services;
using Newtonsoft.Json.Linq;

namespace ConsoleApp2.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController: ApiController, IFileController
    {
        private readonly IBaseService<IFileRequestDto> _service;
        private readonly IFileService<IFileRequestDto> _fileService;

        public FilesController(IBaseService<IFileRequestDto> service)
        {
            _service = new LogInDecoratorService<IFileRequestDto>(new LoggerDecoratorService<IFileRequestDto>(service));
            _fileService = new LogInDecoratorService<IFileRequestDto>(new LoggerDecoratorService<IFileRequestDto>(service));
        }

        [HttpDelete]
        public IHttpActionResult Delete(IFileRequestDto entity)
        {
            throw new System.NotImplementedException();
        }

        [HttpPut]
        public IHttpActionResult Update(IFileRequestDto entity)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById([FromUri]long id)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        public HttpResponseMessage GetAll(IFileRequestDto dto)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        [Route("permission")]
        public HttpResponseMessage CheckUserPermissions()
        {
            var jsonToken = JToken.FromObject(_fileService.CheckUserPermissions(null));
            return new HttpResponseMessage
            {
                Content = new StringContent(jsonToken.ToString(), System.Text.Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

    }
}