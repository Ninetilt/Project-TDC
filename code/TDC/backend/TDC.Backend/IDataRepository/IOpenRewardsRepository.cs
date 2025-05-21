namespace TDC.Backend.IDataRepository
{
    public interface IOpenRewardsRepository
    {
        public void AddOpenRewardForUser(string username, long listId);
        public void RemoveSeenReward(string username, long listId);
        public List<long> GetOpenRewardsForUser(string username);
    }
}
