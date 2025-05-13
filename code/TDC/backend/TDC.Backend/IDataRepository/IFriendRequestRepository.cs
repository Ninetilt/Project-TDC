namespace TDC.Backend.IDataRepository
{
    public interface IFriendRequestRepository
    {
        public void AddFriendRequest(string senderName, string receiverName);
        public void DeleteFriendRequest(string username, string requestName);
        public List<string> GetRequestsForUser(string username);
    }
}
