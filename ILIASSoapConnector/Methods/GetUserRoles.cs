using ILIASSoapConnector.Models;
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
		public async Task<IEnumerable<IliasRole>> GetUserRoles(int userId, string sid = "")
		{
			var soapSession = await GetConnectorSessionAsync();
			if (!String.IsNullOrEmpty(sid))
				soapSession = sid;

			var soapEnvelopeXml = new XmlDocument();
			soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                <soapenv:Header/>
                    <soapenv:Body>
                        <urn:getUserRoles soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <sid xsi:type=""xsd:string"">{0}</sid>
                            <user_id xsi:type=""xsd:int"">{1}</user_id>
                        </urn:getUserRoles>
                    </soapenv:Body>
                </soapenv:Envelope>", soapSession, userId));

			var request = new ILWebRequest(_baseUrl);
			var response = await request.DoRequestAsync(soapEnvelopeXml);

			var roles = IliasToObjectParser.GetUserRolesResponse(response);
			return roles;
		}
	}
}
