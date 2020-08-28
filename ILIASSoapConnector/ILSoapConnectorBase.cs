using System;
using System.Collections.Generic;
using System.Text;

namespace ILIASSoapConnector
{
	public abstract class ILSoapConnectorBase
	{
		protected string _baseUrl;
		protected string _client;
		protected string _soapUser;
		protected string _soapPassword;
		protected string _soapSession;

		public void SetBaseUrl(string baseUrl)
		{
			if (String.IsNullOrEmpty(_baseUrl))
				_baseUrl = baseUrl;
		}

		public void SetCredentials(string soapUser, string soapPassword)
		{
			_soapUser = soapUser;
			_soapPassword = soapPassword;
		}
	}
}
