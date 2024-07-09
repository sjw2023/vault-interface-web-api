using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Exceptions
{
	public class VaultException : CustomException
	{
		public VaultException(string errorCode, string errorMessage) : base()
		{
			base.ErrorCode = errorCode;
			base.ErrorMessage = errorMessage;
		}
	}
}
