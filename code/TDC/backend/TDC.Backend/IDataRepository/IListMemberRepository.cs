namespace TDC.Backend.IDataRepository
{
    public interface IListMemberRepository
    {
        public void AddListMember(long listId, string userId);
        public void RemoveListMember(long listId, string userId);
        public List<string> GetListMembers(long listId);
        public List<long> GetListsForUser(string userId);
    }
}
