namespace TDC.Backend.IDomain.Models
{
    public class RewardingMessageDto
    {
        string Message { get; set; }

        public RewardingMessageDto() { }
        public RewardingMessageDto(string message) {
            Message = message;
        }
    }
}
