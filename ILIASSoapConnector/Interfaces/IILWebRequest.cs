﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ILIASSoapConnector.Interfaces
{
	public interface IILWebRequest
	{
		Task<string> DoRequestAsync(XmlDocument soapEnvelopeXml);
	}
}
