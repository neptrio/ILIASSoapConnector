using ILIASSoapConnector.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ILIASSoapConnector
{
	public partial class ILSoapEndpoint
	{
		public async Task<int> LookupUserAsync(string login)
		{
			var soapSession = await GetConnectorSessionAsync();

			var soapEnvelopeXml = new XmlDocument();
			soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                    <soapenv:Header/>
                        <soapenv:Body>
                            <urn:lookupUser soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                                <sid xsi:type=""xsd:string"">{0}</sid>
                                <user_name xsi:type=""xsd:string"">{1}</user_name>
                            </urn:lookupUser>
                    </soapenv:Body>
                </soapenv:Envelope>", soapSession, login));

			var request = new ILWebRequest(_baseUrl);
			var response = await request.DoRequestAsync(soapEnvelopeXml);

			var userId = IliasToObjectParser.LookupUserResponse(response);

			return userId;
		}
	}
}
