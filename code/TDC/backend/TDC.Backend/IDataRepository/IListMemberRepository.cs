namespace TDC.Backend.IDataRepository
{
    public interface IListMemberRepository
    {
        public List<long> GetMembersForList(long listId);
        public void AddListMember(long listId, long userId);
        public void RemoveListMember(long listId, long userId);
    }
}
