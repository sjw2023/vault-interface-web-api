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
