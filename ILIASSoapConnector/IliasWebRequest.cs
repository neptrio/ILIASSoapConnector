using ILIASSoapConnector.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ILIASSoapConnector
{
	class IliasWebRequest : IIliasWebRequest
	{

		private readonly string _baseUrl;

		public IliasWebRequest(string baseUrl)
		{
			_baseUrl = baseUrl;
		}

		public async Task<string> DoRequestAsync(XmlDocument soapEnvelopeXml)
		{
			HttpWebRequest request = CreateWebRequest();
			using (Stream stream = await request.GetRequestStreamAsync())
			{
				soapEnvelopeXml.Save(stream);
			}

			using (WebResponse response = await request.GetResponseAsync())
			{
				using (StreamReader rd = new StreamReader(response.GetResponseStream()))
				{
					string soapResult = await rd.ReadToEndAsync();
					return soapResult;
				}
			}
		}

		private HttpWebRequest CreateWebRequest()
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(_baseUrl);
			webRequest.Headers.Add(@"SOAP:Action");
			webRequest.ContentType = "text/xml;charset=\"utf-8\"";
			webRequest.Accept = "text/xml";
			webRequest.Method = "POST";
			return webRequest;
		}

	}
}
