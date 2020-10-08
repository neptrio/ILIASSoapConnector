using System;
using System.Collections.Generic;
using System.Text;

namespace ILIASSoapConnector.Models
{
	public class SoapFault
	{
		public string FaultCode { get; }

		public string FaultString { get; }

		public SoapFault(string faultCode, string faultString)
		{
			FaultCode = faultCode;
			FaultString = faultString;
		}
	}
}
