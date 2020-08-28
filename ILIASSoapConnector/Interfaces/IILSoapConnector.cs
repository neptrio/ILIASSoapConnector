using ILIASSoapConnector.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ILIASSoapConnector.Interfaces
{
	public interface IILSoapConnector
	{
		void SetBaseUrl(string baseUrl);
		void SetCredentials(string soapUser, string soapPassword);
		Task<string> LoginAsync(string client, string username, string password);
		Task<IliasUser> SearchUserAsync(string login);
		Task<int> LookupUserAsync(string login);
		Task<IliasUser> GetUserAsync(int userId);
		Task<int> GetUserBySidAsync(string userId);
	}

}
