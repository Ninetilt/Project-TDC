using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface IListRepository
    {
        public long CreateList(ToDoListDbo list);
        public ToDoListDbo? GetById(long listId, long userId);
        public void UpdateListTitle(long listId, string name);
        public void DeleteList(long listId);
        public void FinishList(long listId, long userId);
        public List<long> GetListMembers(long listId);
    }
}
