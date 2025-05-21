namespace TDC.Backend.IDataRepository
{
    public interface IListRewardingRepository
    {
        public void AddNewRewarding(long listId, string message);
        public string? GetRewardingById(long listId);
    }
}
