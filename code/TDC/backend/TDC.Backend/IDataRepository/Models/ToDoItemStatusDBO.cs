namespace TDC.Backend.IDataRepository.Models
{
    public class ToDoItemStatusDbo
    {
        public long ItemId { get; set; }
        public string UserId { get; set; }
        public bool IsDone { get; set; }

        public ToDoItemStatusDbo() { }

        public ToDoItemStatusDbo(long itemId, string userId, bool isDone)
        {
            ItemId = itemId;
            UserId = userId;
            IsDone = isDone;
        }
    }
}
