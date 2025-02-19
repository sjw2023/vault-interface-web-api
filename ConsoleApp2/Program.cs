using System;
using System.Collections.Generic;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Web.Http.Cors;
using Swashbuckle.Application;
using ConsoleApp2.Exceptions;
using ConsoleApp2.Model;

namespace ConsoleApp2
{
	public class Program
	{
		public static Dictionary<string, List<ExcelErrorData>> ErrorInfo { get; private set; }
		static void Main(string[] args)
		{
			string excelFilePath = @"C:\Users\231223\source\repos\ConsoleApp2\ConsoleApp2\Resources\VaultErrorTable.xlsx";
			ExcelService excelService = new ExcelService();
			ErrorInfo = excelService.ReadExcelFile(excelFilePath);

			var config = new HttpSelfHostConfiguration("http://192.168.20.31:8083");

			config.Filters.Add(new CustomExceptionFilter());


			config.EnableSwagger(c => c.SingleApiVersion("v1", "A title for your API")).EnableSwaggerUi();

			//config.services.Replace(typeof(IHttpActionInvoker), new CustomApiControllerActionInvoker());

			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				"API Default", "api/{controller}",
				new { id = RouteParameter.Optional, action=RouteParameter.Optional});

			// Enable CORS for all origins, headers, and methods
			var cors = new EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);

			using (HttpSelfHostServer server = new HttpSelfHostServer(config))
			{
				server.OpenAsync().Wait();
				Console.WriteLine("Press Enter to quit.");
				Console.ReadLine();
			}
		}
	}
}
