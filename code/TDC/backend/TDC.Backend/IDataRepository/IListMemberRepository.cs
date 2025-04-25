namespace TDC.Backend.IDataRepository
{
    public interface IListMemberRepository
    {
        public void AddListMember(long listId, string userId, bool isCreator);
        public void RemoveListMember(long listId, string userId);
        public bool UserIsCreator(long listId, string username);
        public List<string> GetListMembers(long listId);
        public List<long> GetListsForUser(string userId);
    }
}
