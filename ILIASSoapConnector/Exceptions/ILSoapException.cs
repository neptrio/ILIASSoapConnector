using System;
using System.Collections.Generic;
using System.Text;

namespace ILIASSoapConnector.Exceptions
{
	public class ILSoapException : Exception
	{
		public string FaultCode { get; }

		public string FaultError { get; set; }

		public ILSoapException(string message, string faultCode, string faultError): base(message)
		{
			FaultCode = faultCode;
			FaultError = faultError;
		}
	}
}
