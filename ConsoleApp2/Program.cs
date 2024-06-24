using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using VDF = Autodesk.DataManagement.Client.Framework;
using System.Web.Http;


namespace ConsoleApp2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			VDF.Vault.Results.LogInResult result = VDF.Vault.Library.ConnectionManager.LogIn("192.168.10.250", "DTcenter", "DTcenter", "1234", VDF.Vault.Currency.Connections.AuthenticationFlags.Standard, null);

			VDF.Vault.Currency.Connections.Connection connection = result.Connection;
			VDF.Vault.Library.ConnectionManager.LogOut(connection);

			var config = new HttpSelfHostConfiguration("http://localhost:8080");

			config.Routes.MapHttpRoute(
				"API Default", "api/{controller}/{id}",
				new { id = RouteParameter.Optional });

			using (HttpSelfHostServer server = new HttpSelfHostServer(config))
			{
				server.OpenAsync().Wait();
				Console.WriteLine("Press Enter to quit.");
				Console.ReadLine();
			}

		}
	}
}
