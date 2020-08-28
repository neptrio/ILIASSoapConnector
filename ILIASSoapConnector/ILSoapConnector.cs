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
    public partial class ILSoapConnector : ILSoapConnectorBase, IILSoapConnector
    {

        public ILSoapConnector(string baseUrl, string client, string soapUser, string soapPassword) : this(baseUrl, client)
        {
            _soapUser = soapUser;
            _soapPassword = soapPassword;
        }

        public ILSoapConnector(string baseUrl, string client)
        {
            _baseUrl = baseUrl;
            _client = client;
        }

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
