using TDC.Models;

namespace TDC.IRepository;
public interface IListRepository
{
    public string CreateList(ToDoList list);
    public void UpdateList(ToDoList newList, string listId, long userId);
    public void DeleteList(string listId, long userId);
    public ToDoList? GetListById(string listId, long userId);
    public List<ToDoList> GetAllListsForUser(long userId);
}
