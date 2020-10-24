using ILIASSoapConnector.Interfaces;
using ILIASSoapConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ILIASSoapConnector.Parser
{
    public static class IliasToObjectParser
    {
        private const string StringUserIdPattern = "il_0_usr_";

        public static IliasUser SearchUsersResponse(string xml)
        {
            return ParseUsersFromUserXMLResponse(xml).FirstOrDefault();
        }

        /// <summary>
        /// Parst die XML-Antwort von SOAP - getUser(). 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IliasUser GetUserResponse(string xml)
        {
            XDocument doc = XDocument.Parse(xml);

            var userData = doc.Descendants("user_data");
            var userId = int.Parse(userData.Select(c => c.Element("usr_id")).First().Value);
            var login = userData.Select(c => c.Element("login")).First().Value;
            var title = userData.Select(c => c.Element("title")).First().Value;
            var firstname = userData.Select(c => c.Element("firstname")).First().Value;
            var lastname = userData.Select(c => c.Element("lastname")).First().Value;
            var active = userData.Select(c => c.Element("active")).First().Value == "1" ? true : false;

            return new IliasUser
            {
                UserId = userId,
                Login = login,
                Title = title,
                Firstname = firstname,
                Lastname = lastname,
                Active = active            
            };
        }

        /// <summary>
        /// Parst die XML-Antwort von SOAP - loginUser().
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Ilias SessionId</returns>
        public static string LoginResponse(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            var sid = doc.Root.Value;
            return sid;
        }

        /// <summary>
        /// Parst die XML-Antwort im Falle eines Server Error.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Ilias SessionId</returns>
        public static SoapFault ErrorResponse(string xml)
        {
            XNamespace soapEnv = "http://schemas.xmlsoap.org/soap/envelope/";

            XDocument doc = XDocument.Parse(xml);
            XElement fault = doc.Descendants().Where(x => x.Name.LocalName == "Fault").FirstOrDefault();
            var faultCode = fault.Element("faultcode").Value;
            var faultString = fault.Element("faultstring").Value;

            return new SoapFault(faultCode, faultString);
        }

        /// <summary>
        /// Parst die XML-Antwort von SOAP - loginUser().
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Ilias SessionId</returns>
        public static int LookupUserResponse(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            var value = doc.Root.Value;
            var userId = int.Parse(value);
            
            return userId;
        }

        /// <summary>
        /// Parst die XML-Antwort von SOAP - getUserBySid().
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Ilias UserId</returns>
        public static int GetUserBySidResponse(string xml)
        {
            XDocument doc = XDocument.Parse(xml);
            var value = doc.Root.Value;
            var userId = int.Parse(value);

            return userId;
        }

        /// <summary>
        ///  Parst die XML-Antwort von SOAP - getUserRoles().
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IEnumerable<IliasRole> GetUserRolesResponse(string xml)
		{
            var list = new List<IliasRole>();

            var xmlElement = XElement.Parse(xml);
            var t = XDocument.Parse(xmlElement.Value);

            var users = t.Descendants("Object");
            foreach (var user in users)
            {
                var role = new IliasRole();
                role.Type = user.Attribute("type").Value;
                role.ObjectId = Int32.Parse(user.Attribute("obj_id").Value);
                role.Title = user.Element("Title").Value;
                role.Description = user.Element("Description").Value;

                list.Add(role);
            }

            return list;
        }

        private static List<IliasUser> ParseUsersFromUserXMLResponse(string xml)
        {
            var list = new List<IliasUser>();

            var xmlElement = XElement.Parse(xml);
            var t = XDocument.Parse(xmlElement.Value);

            var users = t.Descendants("User");
            foreach (var user in users)
            {
                list.Add(_ParseUserFromXElement(user));
            }

            return list;
        }

        private static IliasUser _ParseUserFromXElement(XElement userElement)
        {
            var user = new IliasUser();            

            user.UserId = _ParseUserIdFromString(userElement.Attribute("Id").Value);
            user.Login = userElement.Element("Login").Value;
            user.Active = Boolean.Parse(userElement.Element("Active").Value);

            return user;
        }

        private static int _ParseUserIdFromString(string stringId)
        {
            return Int32.Parse(stringId.Replace(StringUserIdPattern, ""));
        }


    }
}
