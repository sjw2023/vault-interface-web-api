namespace ConsoleApp2.Exceptions
{
	using Autodesk.Connectivity.WebServices;
	using System;
	using System.Linq;
	using System.Net;
	using System.Net.Http;
	using System.Runtime.Remoting.Messaging;
	using System.Web.Http.Filters;

	public class CustomExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception is VaultServiceErrorException)
			{
				var errorInfo = Program.ErrorInfo[actionExecutedContext.Exception.Message];
				string combinedDescriptions = string.Join(", ", errorInfo.Select(data => $"{data.Name}: {data.Description}"));

				var errorMessage = new System.Web.Http.HttpError(combinedDescriptions)
				{ { "ErrorCode", actionExecutedContext.Exception.Message } };

				actionExecutedContext.Response =
					actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
			}
		}
	}
}
