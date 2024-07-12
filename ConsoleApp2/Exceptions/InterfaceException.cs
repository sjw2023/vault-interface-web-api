using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Exceptions
{
	public	class InterfaceException : CustomException
	{
		public InterfaceException(int errorCode, string errorMessage) : base()
		{
			base.ErrorCode = errorCode;
			base.ErrorMessage = errorMessage;
		}
	}
}
