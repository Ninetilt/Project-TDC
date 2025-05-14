using TDC.Backend.IDomain.Models;

namespace TDC.Backend.IDomain
{
    public interface IAccountHandler
    {
        public bool RegisterUser(AccountSavingDto accountData);
        public bool UpdateUsername(string oldUsername, string newUsername);
        public bool UpdateUserDescription(string username, string description);
        public bool UpdateEmail(string username, string email);
        public bool UpdatePassword(string username, string password);
        public bool LoginWithMail(string email, string password);
        public bool LoginWithUsername(string username, string password);
        public AccountLoadingDto GetAccountByUsername(string username);
        public List<string> GetFriendsForUser(string username);
        public List<string> GetRequestsForUser(string username);
        public Task AcceptFriendRequest(string username, string requestName);
        public Task DenyFriendRequest(string username, string requestName);
        public Task SendFriendRequest(string sender, string receiver);
        public Task CancelFriendRequest(string sender, string receiver);
    }
}
