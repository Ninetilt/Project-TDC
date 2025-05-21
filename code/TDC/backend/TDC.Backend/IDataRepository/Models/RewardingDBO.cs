namespace TDC.Backend.IDataRepository.Models
{
    public class RewardingDbo
    {
        public long ListId { get; set; }
        public string RewardingMessage { get; set; }

        public RewardingDbo() { }

        public RewardingDbo(long listId, string rewardingMessage) {
            RewardingMessage = rewardingMessage;
            ListId = listId;
        }
    }
}
