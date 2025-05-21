namespace TDC.Backend.IDomain.Models
{
    public class RewardingMessageDto
    {
        string Message { get; set; }
        long ListId { get; set; }

        public RewardingMessageDto() { }
        public RewardingMessageDto(string message, long listId)
        {
            Message = message;
            ListId = listId;
        }
    }
}
