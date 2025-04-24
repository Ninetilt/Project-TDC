using TDC.Backend.IDataRepository;

namespace TDC.Backend.DataRepository
{
    public class ListMemberRepository : IListMemberRepository
    {
        public List<long> GetMembersForList(long listId)
        {
            throw new NotImplementedException();
        }
        public void AddListMember(long listId, long userId)
        {
            throw new NotImplementedException();
        }
        public void RemoveListMember(long listId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
