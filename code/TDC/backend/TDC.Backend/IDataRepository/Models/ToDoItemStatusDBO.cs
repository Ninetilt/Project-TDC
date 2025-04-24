namespace TDC.Backend.IDataRepository.Models
{
    public class ToDoItemStatusDbo(long itemId, string userId, bool isDone)
    {
        public long ItemId { get; set; } = itemId;
        public string UserId { get; set; } = userId;
        public bool IsDone { get; set; } = isDone;
    }
}
