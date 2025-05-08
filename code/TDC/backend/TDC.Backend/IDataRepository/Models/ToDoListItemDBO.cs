namespace TDC.Backend.IDataRepository.Models;

public class ToDoListItemDbo(long id, long listId, string description, uint effort)
{
    public long Id { get; set; } = id;
    public long ListId { get; set; } = listId;
    public string Description { get; set; } = description;
    public uint Effort { get; set; } = effort;
}
