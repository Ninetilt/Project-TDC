namespace TDC.Backend.IDataRepository.Models;

public class ToDoListDbo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsCollaborative { get; set; }
    public bool IsFinished { get; set; }

    public ToDoListDbo() { }

    public ToDoListDbo(long id, string name, bool isCollaborative, bool isFinished)
    {
        Id = id;
        Name = name;
        IsCollaborative = isCollaborative;
        IsFinished = isFinished;
    }
}

