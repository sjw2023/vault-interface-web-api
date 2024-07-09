namespace ConsoleApp2.Exceptions
{
	using Autodesk.Connectivity.WebServices;
	using System;
	using System.Net;
	using System.Net.Http;
	using System.Web.Http.Filters;

	public class CustomExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception is VaultServiceErrorException)
			{
				Console.WriteLine("VaultServiceErrorException");

				VaultException vaultException = new VaultException(actionExecutedContext.Exception.Message, "202020");
				
				var errorMessage = new System.Web.Http.HttpError(vaultException.ErrorMessage)
				{ { "ErrorCode",vaultException.ErrorCode } };

				actionExecutedContext.Response =
					actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
			}
		}
	}
}
