namespace TDC.Backend.IDomain.Models
{
    public class ToDoListItemSavingDto(string description, int effort)
    {
        public string Description { get; set; } = description;
        public int Effort { get; set; } = effort;
    }
}
