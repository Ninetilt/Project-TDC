namespace TDC.Backend.IDataRepository
{
    public interface IListMemberRepository
    {
        public void AddListMember(long listId, long userId);
        public void RemoveListMember(long listId, long userId);
        public List<long> GetListMembers(long listId);
        public List<long> GetListsForUser(long userId);
    }
}
