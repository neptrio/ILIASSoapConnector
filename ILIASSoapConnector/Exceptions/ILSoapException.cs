using System;
using System.Collections.Generic;
using System.Text;

namespace ILIASSoapConnector.Exceptions
{
	public class ILSoapException : Exception
	{
		public ILSoapException(string message): base(message)
		{

		}
	}
}
