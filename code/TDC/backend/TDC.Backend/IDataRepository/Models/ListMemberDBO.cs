namespace TDC.Backend.IDataRepository.Models
{
    public class ListMemberDbo(long listId, string userId, bool isCreator)
    {
        public long ListId { get; set; } = listId;
        public string UserId { get; set; } = userId;
        public bool IsCreator { get; set; } = isCreator;
    }
}
