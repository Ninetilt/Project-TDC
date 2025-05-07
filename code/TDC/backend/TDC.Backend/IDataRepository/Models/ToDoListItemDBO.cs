namespace TDC.Backend.IDataRepository.Models;

public class ToDoListItemDbo(long itemId, long listId, string description, uint effort)
{
    public long ItemId { get; set; } = itemId;
    public long ListId { get; set; } = listId;
    public string Description { get; set; } = description;
    public uint Effort { get; set; } = effort;
}
