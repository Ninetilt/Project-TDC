namespace TDC.Backend.IDataRepository
{
    public interface IFriendRepository
    {
        public void AddFriend(string username, string friendName);
        public void RemoveFriend(string username, string friendName);
        public List<string> GetFriendsForUser(string username);
    }
}
