using ILIASSoapConnector.Interfaces;
using ILIASSoapConnector.Models;
using ILIASSoapConnector.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace ILIASSoapConnector
{
	public partial class ILSoapEndpoint : ILSoapEndpointBase, IILSoapEndpoint
	{

		public ILSoapEndpoint(string baseUrl, string client, string soapUser, string soapPassword) : this(baseUrl, client)
		{
			_soapUser = soapUser;
			_soapPassword = soapPassword;
		}

		public ILSoapEndpoint(string baseUrl, string client)
		{
			_baseUrl = baseUrl;
			_client = client;
		}


		/// <summary>
		/// Returns the session needed to communicate with protected methods.
		/// If no session is available an authentication is performed. 
		/// An expired session is not updated or checked. TODO
		/// </summary>
		/// <returns></returns>
		private async Task<string> GetConnectorSessionAsync()
		{
			if (_client == null)
				throw new ArgumentNullException("Client wird benötigt");

			if (_soapUser == null)
				throw new ArgumentNullException("Client wird benötigt");

			if (_soapPassword == null)
				throw new ArgumentNullException("Client wird benötigt");

			if (_soapSession == null)
				_soapSession = await LoginAsync(_client, _soapUser, _soapPassword);
			return _soapSession;
		}

	}
}
