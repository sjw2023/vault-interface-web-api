using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Exceptions
{
	public class CustomException : Exception
	{
		public virtual string ErrorCode { get; set; }
		public virtual string ErrorMessage { get; set; } = string.Empty;
	}
}
