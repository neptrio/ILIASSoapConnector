using ILIASSoapConnector.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ILIASSoapConnector.Interfaces
{
	public interface IILSoapEndpoint
	{
		void SetBaseUrl(string baseUrl);
		void SetCredentials(string soapUser, string soapPassword);
		Task<string> LoginAsync(string client, string username, string password);
		Task<string> LoginLDAPAsync(string client, string username, string password);
		Task<IliasUser> SearchUserAsync(string login);
		Task<int> LookupUserAsync(string login);
		Task<IliasUser> GetUserAsync(int userId);
		Task<IEnumerable<IliasRole>> GetUserRoles(int userId);
		Task<int> GetUserBySidAsync(string userId);
		Task<bool> SendMail(string to, string cc, string bcc, string sender, string subject, string message);
	}

}
