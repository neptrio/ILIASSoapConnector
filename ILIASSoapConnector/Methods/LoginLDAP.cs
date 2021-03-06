﻿using ILIASSoapConnector.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ILIASSoapConnector
{
	public partial class ILSoapEndpoint
	{
		public async Task<string> LoginLDAPAsync(string client, string username, string password)
		{
			var soapEnvelopeXml = new XmlDocument();
			soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                  <soapenv:Header/>
                    <soapenv:Body>
                        <urn:loginLDAP soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <client xsi:type=""xsd:string"">{0}</client>
                            <username xsi:type=""xsd:string"">{1}</username>
                            <password xsi:type=""xsd:string""><![CDATA[{2}]]></password>
                        </urn:loginLDAP>
                    </soapenv:Body>
               </soapenv:Envelope>", client, username, password));

			var request = new ILWebRequest(_baseUrl);
			var response = await request.DoRequestAsync(soapEnvelopeXml);

			var sid = IliasToObjectParser.LoginResponse(response);

			return sid;
		}
	}
}
