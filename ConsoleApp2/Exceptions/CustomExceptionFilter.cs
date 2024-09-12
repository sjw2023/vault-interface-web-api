using System.Linq;
using System.Net;

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
					actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.OK, errorMessage);
			}
			if (actionExecutedContext.Exception is InterfaceException) { 
				var exception = actionExecutedContext.Exception as InterfaceException;
				var errorMessage = new System.Web.Http.HttpError("OK"){
					{ "ErrorCode", exception.ErrorCode },
					{ "ErrorMessage", exception.ErrorMessage} };
				actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.OK, errorMessage);
			}
		}
	}
}
