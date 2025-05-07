namespace TDC.Backend.IDataRepository.Models;

public class ToDoListDbo
{
    public long ListId { get; set; }
    public string Name { get; set; }
    public bool IsCollaborative { get; set; }
    public bool IsFinished { get; set; }

    public ToDoListDbo() { }

    public ToDoListDbo(long listId, string name, bool isCollaborative, bool isFinished)
    {
        ListId = listId;
        Name = name;
        IsCollaborative = isCollaborative;
        IsFinished = isFinished;
    }
}

