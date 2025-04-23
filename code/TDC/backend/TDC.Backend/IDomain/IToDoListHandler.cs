using TDC.Backend.IDomain.Models;

namespace TDC.Backend.IDomain
{
    public interface IToDoListHandler
    {
        public long CreateList(ToDoListDto newList);
        public void UpdateListTitle(long listId, string newTitle);
        public void DeleteList(long listId, long userId);
        public void FinishList(long listId, long userId);
    }
}
