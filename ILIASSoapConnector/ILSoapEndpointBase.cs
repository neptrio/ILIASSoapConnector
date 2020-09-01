using System;
using System.Collections.Generic;
using System.Text;

namespace ILIASSoapConnector
{
	public abstract class ILSoapEndpointBase
	{
		protected string _baseUrl;
		protected string _client;
		protected string _soapUser;
		protected string _soapPassword;
		protected string _soapSession;

		/// <summary>
		/// Sets the base url, if the base url is not already set.
		/// </summary>
		/// <param name="baseUrl"></param>
		public void SetBaseUrl(string baseUrl)
		{
			if (String.IsNullOrEmpty(_baseUrl))
				_baseUrl = baseUrl;
		}

		/// <summary>
		/// Sets the credentials the endpoint is using to call protected methods.
		/// </summary>
		/// <param name="soapUser"></param>
		/// <param name="soapPassword"></param>
		public void SetCredentials(string soapUser, string soapPassword)
		{
			_soapUser = soapUser;
			_soapPassword = soapPassword;
		}
	}
}
