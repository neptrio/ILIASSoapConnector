﻿using ILIASSoapConnector.Models;
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
        /// <summary>
        /// Achtung. Das liefert mehrere User in der SOAP Response zurück. Überarbeiten. Sucht nur nach Login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<IliasUser> SearchUserAsync(string login, string sid = "")
        {

            var session = await GetConnectorSessionAsync();
            if (!String.IsNullOrEmpty(sid))
                session = sid;

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
            </s:Envelope>", session, term, login));

            var request = new ILWebRequest(_baseUrl);
            var response = await request.DoRequestAsync(soapEnvelopeXml);

            var user = IliasToObjectParser.SearchUsersResponse(response);
            return user;
        }
    }
}
