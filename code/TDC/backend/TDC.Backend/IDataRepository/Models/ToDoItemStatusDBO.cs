namespace TDC.Backend.IDataRepository.Models
{
    public class ToDoItemStatusDbo(long itemId, long userId, bool isDone)
    {
        public long ItemId { get; set; } = itemId;
        public long UserId { get; set; } = userId;
        public bool IsDone { get; set; } = isDone;
    }
}
