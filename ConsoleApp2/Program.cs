using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using VDF = Autodesk.DataManagement.Client.Framework;
using System.Web.Http;
using Swashbuckle.Application;
using ConsoleApp2.Exceptions;
using System.Web.Http.Controllers;

namespace ConsoleApp2
{
	public class Program
	{
		static void Main(string[] args)
		{
			var config = new HttpSelfHostConfiguration("http://192.168.20.31:8080");
			
			config.Filters.Add(new CustomExceptionFilter());

			config.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API")).EnableSwaggerUi();

			//config.Services.Replace(typeof(IHttpActionInvoker), new CustomApiControllerActionInvoker());

			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				"API Default", "api/{controller}/{action}/{id}",
				new { id = RouteParameter.Optional, action=RouteParameter.Optional});

			using (HttpSelfHostServer server = new HttpSelfHostServer(config))
			{
				server.OpenAsync().Wait();
				Console.WriteLine("Press Enter to quit.");
				Console.ReadLine();
			}
		}
	}
}
