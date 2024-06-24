using ConsoleApp2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsoleApp2.Results
{
	public class ItemsResult : IHttpActionResult
	{
		Item[] _value;
		HttpRequestMessage _request;

		public ItemsResult(Item [] value, HttpRequestMessage request)
		{
			_value = value;
			_request = request;
		}
		public Task<HttpResponseMessage> ExecuteAsync(System.Threading.CancellationToken cancellationToken)
		{
			var response = new HttpResponseMessage()
			{
				Content = new StringContent(_value.ToString()),
				RequestMessage = _request
			};
			return Task.FromResult(response);
		}
	}
}
