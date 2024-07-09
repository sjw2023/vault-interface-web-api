//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.ExceptionHandling;
//using System.Web.Http.Filters;

//namespace ConsoleApp2.Exceptions
//{
//	public class VIExceptionHanlder:ExceptionHandler, ExceptionFilterAttribute
//	{
//		public override void Handle(ExceptionHandlerContext context)
//		{
//			context.Result = new VIErrorResult
//			{
//				RequestMessage = context.ExceptionContext.Request,
//				Content = "An error occurred, please try again or contact your system administrator."
//			};
//			Console.WriteLine(context.Exception.Message);
//		}
//		private class VIErrorResult : IHttpActionResult
//		{
//			public HttpRequestMessage RequestMessage { get; set; }
//			public string Content { get; set; }
//			public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//			{
//				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
//				response.Content = new StringContent(Content);
//				response.RequestMessage = RequestMessage;
//				return Task.FromResult(response);
//			}
//		}
//	}
//}
