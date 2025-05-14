namespace TDC.Backend.IDataRepository.Models;

public class ToDoListItemDbo
{
    public long Id { get; set; }
    public long ListId { get; set; }
    public string Description { get; set; }
    public int Effort { get; set; }

    public ToDoListItemDbo() {}

    public ToDoListItemDbo(long id, long listId, string description, int effort)
    {
        Id = id;
        ListId = listId;
        Description = description;
        Effort = effort;
    }
}
