namespace TDC.Backend.IDataRepository.Models
{
    public class ListMemberDbo(long listId, long userId)
    {
        public long ListId { get; set; } = listId;
        public long UserId { get; set; } = userId;
    }
}
