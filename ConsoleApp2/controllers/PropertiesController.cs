using System;
using System.Net;
using ConsoleApp2.Model;
using ConsoleApp2.Results;
using ConsoleApp2.Services;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.Extensions.Logging;

namespace ConsoleApp2.Controllers
{
    [RoutePrefix("api/properties")]
    public class PropertiesController : ApiController, IBaseController<PropertyDTO>
    {
        private readonly IBaseService<PropertyDTO> _service;
        private readonly IPropertyService<PropertyDTO> _propertyService;

        public PropertiesController(IBaseService<PropertyDTO> service)
        {
            _service = new LogInDecoratorService<PropertyDTO>(new LoggerDecoratorService<PropertyDTO>(service));
            _propertyService = new LogInDecoratorService<PropertyDTO>(new LoggerDecoratorService<PropertyDTO>(service));
        }

        public PropertiesController()
        {
            _service = new LogInDecoratorService<PropertyDTO>(new LoggerDecoratorService<PropertyDTO>(new PropertyService<PropertyDTO>()));
            _propertyService = new LogInDecoratorService<PropertyDTO>(new LoggerDecoratorService<PropertyDTO>(new PropertyService<PropertyDTO>()));
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] PropertyDTO entity)
        {
            _service.Add(entity, null);
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromBody] PropertyDTO entity)
        {
            _service.Delete(entity, null);
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] PropertyDTO entity)
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

        [HttpPost]
        [Route("all")]
        public HttpResponseMessage GetAll([FromBody] PropertyDTO dto)
        {
            var json = JToken.FromObject(_service.GetAll(dto.m_PropertyRequestDTO.m_IdDTO.Ids, null));
            return new HttpResponseMessage
            {
                Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        [HttpGet]
        public HttpResponseMessage GetPropertiesOfItem()
        {
            var json = JToken.FromObject(_propertyService.GetPropertiesOfItem(null));
            return new HttpResponseMessage
            {
                Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }

        //TODO : Remove this method
        [HttpGet]
        [Route("property-values")]
        public HttpResponseMessage GetPropertyValues()
        {
            Console.WriteLine("Getting property values");
            _propertyService.GetPropertyValues(null);
            return new HttpResponseMessage
            {
                // Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json"),
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
