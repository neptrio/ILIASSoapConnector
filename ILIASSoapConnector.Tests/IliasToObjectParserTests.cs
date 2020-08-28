using ILIASSoapConnector.Models;
using ILIASSoapConnector.Parser;
using System;
using Xunit;

namespace IliasConnector.Tests
{
    public class IliasToObjectParserTests
    {
        [Fact]
        public void ParseLoginString_From_XML()
        {

            var sid_excpected = "djeabippnqbrg9oa6tfkav6rf2::elearning";

            var xml =
                @"<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns1=""urn:ilUserAdministration"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"">
                    <SOAP-ENV:Body>
                        <ns1:loginResponse>
                            <sid xsi:type =""xsd:string"">djeabippnqbrg9oa6tfkav6rf2::elearning</sid>
                        </ns1:loginResponse>
                    </SOAP-ENV:Body>
                  </SOAP-ENV:Envelope>";

            var sid_actual = IliasToObjectParser.LoginResponse(xml);
            Assert.Equal(sid_excpected, sid_actual);
        }

        [Fact]
        public void ParseIliasUserObject_From_XMLGetUser()
        {

            var user_excpected = new IliasUser();
            user_excpected.UserId = 100122;
            user_excpected.Active = true;
            user_excpected.Title = "";
            user_excpected.Firstname = "Peter";
            user_excpected.Lastname = "Schneider";
            user_excpected.Login = "peterschneid";


            var xml = @"<SOAP-ENV:Envelope SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ns1=""urn:ilUserAdministration"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"">
                        <SOAP-ENV:Body>
                            <ns1:getUserResponse>
                                <user_data xsi:type=""ns1:ilUserData"">
                                <usr_id xsi:type=""xsd:int"">100122</usr_id>
                                <login xsi:type=""xsd:string"">peterschneid</login>
                                <passwd xsi:type=""xsd:string"">$2y$10$4xE.ANQmLI5aVK8.vM2GOePvAbseuW2UNEOLr4gML.8NXmfyXQON2</passwd>
                                <firstname xsi:type=""xsd:string"">Peter</firstname>
                                <lastname xsi:type =""xsd:string"">Schneider</lastname>
                                <title xsi:type=""xsd:string""/>
                                <gender xsi:type=""xsd:string"">n</gender>
                                <email xsi:type=""xsd:string"">schneider.peter@web.de</email>
                                <second_email xsi:type =""xsd:string""/>
                                <institution xsi:type=""xsd:string""/>
                                <street xsi:type=""xsd:string""/>
                                <city xsi:type=""xsd:string""/>
                                <zipcode xsi:type=""xsd:string""/>
                                <country xsi:type=""xsd:string""/>
                                <phone_office xsi:type=""xsd:string""/>   
                                <last_login xsi:type=""xsd:string"">2020-01-14 11:07:42</last_login>                                     
                                <last_update xsi:type=""xsd:string"">2020-01-13 17:32:55</last_update>
                                <create_date xsi:type=""xsd:string"">2020-01-13 17:32:55</create_date>
                                <hobby xsi:type=""xsd:string""/>
                                <department xsi:type=""xsd:string""/>
                                <phone_home xsi:type=""xsd:string""/>
                                <phone_mobile xsi:type=""xsd:string""/>
                                <fax xsi:type=""xsd:string""/>
                                <time_limit_owner xsi:type=""xsd:int"">7</time_limit_owner>
                                <time_limit_unlimited xsi:type=""xsd:int"">1</time_limit_unlimited>
                                <time_limit_from xsi:nil=""true""/>
                                <time_limit_until xsi:nil=""true""/>
                                <time_limit_message xsi:type=""xsd:int"">0</time_limit_message>
                                <referral_comment xsi:type=""xsd:string""/>
                                <matriculation xsi:type=""xsd:string""/>
                                <active xsi:type=""xsd:int"">1</active>
                                <accepted_agreement xsi:type=""xsd:boolean"">true</accepted_agreement>
                                <approve_date xsi:type=""xsd:string"">2020-01-13 17:32:55</approve_date>
                                <user_skin xsi:type=""xsd:string"">default_skin</user_skin>
                                <user_style xsi:type=""xsd:string"">default_skin_style_default</user_style>
                                <user_language xsi:type=""xsd:string"">de</user_language>
                                <import_id xsi:nil=""true""/>
                                </user_data>
                            </ns1:getUserResponse>
                        </SOAP-ENV:Body>
                        </SOAP-ENV:Envelope>";

            var user_actual = IliasToObjectParser.GetUserResponse(xml);
            Assert.Equal(user_excpected.UserId, user_actual.UserId);
            Assert.Equal(user_excpected.Login, user_actual.Login);
            Assert.Equal(user_excpected.Title, user_actual.Title);
            Assert.Equal(user_excpected.Firstname, user_actual.Firstname);
            Assert.Equal(user_excpected.Lastname, user_actual.Lastname);
            Assert.Equal(user_excpected.Active, user_actual.Active);
        }
    }
}
