namespace TDC.Backend.IDataRepository.Models
{
    public class ListInvitationDbo
    {
        public ListInvitationDbo() { }
        public ListInvitationDbo(string forUser, string fromUser, long listId)
        {
            ForUser = forUser;
            FromUser = fromUser;
            ListId = listId;
        }

        public string ForUser { get; set; }
        public string FromUser { get; set; }
        public long ListId { get; set; }
    }
}
