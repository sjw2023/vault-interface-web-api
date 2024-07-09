using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace ConsoleApp2.Exceptions
{
	public class CustomApiControllerActionInvoker : ApiControllerActionInvoker
	{
		public override Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			var result = base.InvokeActionAsync(actionContext, cancellationToken);

			if (result.Exception != null && result.Exception.GetBaseException() != null)
			{
				var baseException = result.Exception.InnerExceptions[0];//result.Exception.GetBaseException();

				if (baseException is VaultException)
				{
					var baseExcept = baseException as VaultException;
					var errorMessagError = new System.Web.Http.HttpError(baseExcept.ErrorMessage)
					{ { "ErrorCode", baseExcept.ErrorCode } };
					return Task.Run<HttpResponseMessage>(() =>
					actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessagError));
				}
			}
			return base.InvokeActionAsync(actionContext, cancellationToken);
		}
	}
}
