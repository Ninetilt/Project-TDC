namespace TDC.Backend.IDataRepository.Models
{
    public class ListMemberDbo(long listId, string userId)
    {
        public long ListId { get; set; } = listId;
        public string UserId { get; set; } = userId;
    }
}
