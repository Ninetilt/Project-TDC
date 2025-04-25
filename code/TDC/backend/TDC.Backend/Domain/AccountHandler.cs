using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class AccountHandler : IAccountHandler
    {
        public bool AcceptFriendRequest(string username, string requestName)
        {
            throw new NotImplementedException();
        }

        public bool CancelFriendRequest(string sender, string receiver)
        {
            throw new NotImplementedException();
        }

        public bool DenyFriendRequest(string username, string requestName)
        {
            throw new NotImplementedException();
        }

        public AccountLoadingDto GetAccountData(string username)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFriendsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public List<string> GetRequestsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool LoginWithMail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool LoginWithUsername(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(AccountSavingDto accountData)
        {
            throw new NotImplementedException();
        }

        public bool SendFriendRequest(string sender, string receiver)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmail(string username, string email)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserDescription(string username, string description)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUsername(string oldUsername, string newUsername)
        {
            throw new NotImplementedException();
        }
    }
}
