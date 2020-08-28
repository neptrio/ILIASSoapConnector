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
    public class ILSoapConnector : IILSoapConnector
    {

        private string _baseUrl;
        private string _soapUser;
        private string _soapPassword;
        private string _soapSession;

        public ILSoapConnector(string baseUrl, string soapUser, string soapPassword) : this(baseUrl)
        {
            _soapUser = soapUser;
            _soapPassword = soapPassword;
        }

        public ILSoapConnector(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void SetBaseUrl(string baseUrl)
        {
            if (String.IsNullOrEmpty(_baseUrl))
                _baseUrl = baseUrl;
        }

        public void SetCredentials(string soapUser, string soapPassword)
        {
            _soapUser = soapUser;
            _soapPassword = soapPassword;
        }

        /// <summary>
        /// Achtung. Das liefert meherer User in der SOAP Response zurück. Überarbeiten.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<IliasUser> SearchUserAsync(string login)
        {

            if (_soapSession == null)
                _soapSession = await LoginAsync("elearning", _soapUser, _soapPassword);

            var term = "login";

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                <s:Body s:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <q1:searchUser xmlns:q1=""urn:ilUserAdministration"">
                        <sid xsi:type=""xsd:string"">{0}</sid>
                        <key_fields href=""#id1""/>
                        <query_operator xsi:type=""xsd:string"">=</query_operator>
                        <key_values href=""#id2""/>
                        <attach_roles xsi:type=""xsd:int"">0</attach_roles>
                        <active xsi:type=""xsd:int"">1</active>
                    </q1:searchUser>
                    <q2:Array id=""id1"" q2:arrayType=""xsd:string[1]"" xmlns:q2=""http://schemas.xmlsoap.org/soap/encoding/"">
                        <Item>{1}</Item>
                    </q2:Array>
                    <q3:Array id=""id2"" q3:arrayType=""xsd:string[1]"" xmlns:q3=""http://schemas.xmlsoap.org/soap/encoding/"">
                        <Item>{2}</Item>
                    </q3:Array>
                </s:Body>
            </s:Envelope>", _soapSession, term, login));

            var request = new IliasWebRequest(_baseUrl);
            var response = await request.DoRequestAsync(soapEnvelopeXml);

            var user = IliasToObjectParser.SearchUsersResponse(response);
            return user;
        }

        public async Task<int> GetUserBySid(string sid)
		{
            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                <soapenv:Header/>
                    <soapenv:Body>
                        <urn:getUserIdBySid soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <sid xsi:type=""xsd:string"">{0}</sid>
                        </urn:getUserIdBySid>
                    </soapenv:Body>
                </soapenv:Envelope>", sid));

            var request = new IliasWebRequest(_baseUrl);
            var response = await request.DoRequestAsync(soapEnvelopeXml);

            var user = IliasToObjectParser.GetUserBySidResponse(response);
            return user;
        }


        public async Task<IliasUser> GetUserAsync(int userId)
        {

            if (_soapSession == null)
                _soapSession = await LoginAsync("elearning", _soapUser, _soapPassword);

            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                <soapenv:Header/>
                    <soapenv:Body>
                        <urn:getUser soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <sid xsi:type=""xsd:string"">{0}</sid>
                            <user_id xsi:type=""xsd:int"">{1}</user_id>
                        </urn:getUser>
                    </soapenv:Body>
                </soapenv:Envelope>", _soapSession, userId));

            var request = new IliasWebRequest(_baseUrl);
            var response = await request.DoRequestAsync(soapEnvelopeXml);

            var user = IliasToObjectParser.GetUserResponse(response);
            return user;
        }

        public async Task<string> LoginAsync(string client, string username, string password)
        {
            var soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:ilUserAdministration"">
                  <soapenv:Header/>
                    <soapenv:Body>
                        <urn:login soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                            <client xsi:type=""xsd:string"">{0}</client>
                            <username xsi:type=""xsd:string"">{1}</username>
                            <password xsi:type=""xsd:string"">{2}</password>
                        </urn:login>
                    </soapenv:Body>
               </soapenv:Envelope>", client, username, password));

            var request = new IliasWebRequest(_baseUrl);
            var response = await request.DoRequestAsync(soapEnvelopeXml);

            var sid = IliasToObjectParser.LoginResponse(response);

            return sid;
        }

        public async Task<int> LookupUserAsync(string login)
        {
            if (_soapSession == null)
                _soapSession = await LoginAsync("elearning", _soapUser, _soapPassword);

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
                </soapenv:Envelope>", _soapSession, login));

            var request = new IliasWebRequest(_baseUrl);
            var response = await request.DoRequestAsync(soapEnvelopeXml);

            var userId = IliasToObjectParser.LookupUserResponse(response);

            return userId;
        }
    }
}
