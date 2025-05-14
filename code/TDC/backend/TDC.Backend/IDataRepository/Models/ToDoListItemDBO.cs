namespace TDC.Backend.IDataRepository.Models;

public class ToDoListItemDbo(long itemId, string description, uint effort)
{
    public long ItemId { get; set; } = itemId;
    public string Description { get; set; } = description;
    public uint Effort { get; set; } = effort;
}
