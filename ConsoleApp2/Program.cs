using System;
using System.Collections.Generic;
using System.Web.Http.SelfHost;
using System.Web.Http;
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
