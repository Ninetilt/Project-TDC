namespace TDC.Backend.IDomain.Models
{
    public class ToDoListSavingDto(string name, bool isCollaborative)
    {
        public string Name { get; } = name;
        public bool IsCollaborative { get; } = isCollaborative;
    }
}
