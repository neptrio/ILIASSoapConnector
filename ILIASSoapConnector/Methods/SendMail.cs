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
		public async Task<bool> SendMail(string to, string cc, string bcc, string sender, string subject, string message)
		{
			var soapSession = await GetConnectorSessionAsync();

			var soapEnvelopeXml = new XmlDocument();
			soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                  <soapenv:Header/>
                    <soapenv:Body>
                        <urn:sendMail soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <sid xsi:type=""xsd:string"">{0}</sid>
                            <rcp_to xsi:type=""xsd:string"">{1}</rcp_to>
                            <rcp_cc xsi:type=""xsd:string"">{2}</rcp_cc>
                            <rcp_bcc xsi:type=""xsd:string"">{3}</rcp_bcc>
                            <sender xsi:type=""xsd:string"">{4}</sender>
                            <subject xsi:type=""xsd:string"">{5}</subject>
                            <message xsi:type=""xsd:string"">{6}</message>
                            <attachments xsi:type=""xsd:string""></attachments>
                        </urn:sendMail>
                    </soapenv:Body>
               </soapenv:Envelope>", soapSession, to, cc, bcc, sender, subject, message));

			var request = new ILWebRequest(_baseUrl);
			var response = await request.DoRequestAsync(soapEnvelopeXml);

			return IliasToObjectParser.SendEmailResponse(response);
		}
	}
}
