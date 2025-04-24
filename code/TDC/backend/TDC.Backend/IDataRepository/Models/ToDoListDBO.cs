namespace TDC.Backend.IDataRepository.Models;

public class ToDoListDbo(long listId, long userId, string name, bool isCollaborative, bool isFinished)
{
    public long ListId { get; set; } = listId;
    public long UserId { get; set; } = userId;
    public string Name { get; set; } = name;
    public bool IsCollaborative { get; set; } = isCollaborative;
    public bool IsFinished { get; set; } = isFinished;
}
