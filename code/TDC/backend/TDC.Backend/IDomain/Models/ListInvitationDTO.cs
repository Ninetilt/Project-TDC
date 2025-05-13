namespace TDC.Backend.IDomain.Models
{
    public class ListInvitationDto
    {
        public ListInvitationDto(string fromUser, long listId) {
            FromUser = fromUser;
            ListId = listId;
        }
        public string FromUser { get; set; }
        public long ListId { get; set; }
    }
}
