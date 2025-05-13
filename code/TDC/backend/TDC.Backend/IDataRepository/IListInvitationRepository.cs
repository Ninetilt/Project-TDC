using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface IListInvitationRepository
    {
        public void AddListInvitation(string forUser, string fromUser, long listId);
        public void DeleteListInvitation(string forUser, string fromUser, long listId);
        public List<ListInvitationDbo> GetInvitationsForUser(string username);
    }
}
