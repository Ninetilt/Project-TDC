namespace TDC.Backend.IDataRepository.Models;

public class ToDoListDbo(long listId, string name, bool isCollaborative, bool isFinished)
{
    public long ListId { get; set; } = listId;
    public string Name { get; set; } = name;
    public bool IsCollaborative { get; set; } = isCollaborative;
    public bool IsFinished { get; set; } = isFinished;
}
