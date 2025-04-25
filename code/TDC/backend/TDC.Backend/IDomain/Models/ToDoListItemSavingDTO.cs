namespace TDC.Backend.IDomain.Models
{
    public class ToDoListItemSavingDto(string description, uint effort)
    {
        public string Description { get; set; } = description;
        public uint Effort { get; set; } = effort;
    }
}
