using ILIASSoapConnector.Exceptions;
using ILIASSoapConnector.Interfaces;
using ILIASSoapConnector.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ILIASSoapConnector
{
	class ILWebRequest : IILWebRequest
	{

		private readonly string _baseUrl;

		public ILWebRequest(string baseUrl)
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

			try
			{
				using (WebResponse response = await request.GetResponseAsync())
				{
					return await ReadStreamAsync(response);
				}
			}
			catch (WebException e)
			{
				//For many possible errors ILIAS returns an HTTP 500 error that throws an exception. 
				//In order to know what happens we have to intercept the error and read the content.
				var response = await ReadStreamAsync(e.Response);
				try
				{
					var errorMessage = IliasToObjectParser.ErrorResponse(response);
					throw new ILSoapException(e.Message, errorMessage.FaultCode, errorMessage.FaultString);
				}
				catch (Exception)
				{
					//If we cannot parse the error there is another problem. 
					//In this case we throw the original exception.
					throw e;
				}
			}
		}


		private async Task<string> ReadStreamAsync(WebResponse response)
		{
			using (StreamReader rd = new StreamReader(response.GetResponseStream()))
			{
				string soapResult = await rd.ReadToEndAsync();
				return soapResult;
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
