using ConsoleApp2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Exceptions
{
	public class VaultException : CustomException
	{
		public List<ExcelErrorData> ErrorData { get; set; }
		public VaultException(string errorCode, List<ExcelErrorData> errorData) : base()
		{
			base.ErrorCode = errorCode;
			ErrorData = errorData;
		}
	}
}
